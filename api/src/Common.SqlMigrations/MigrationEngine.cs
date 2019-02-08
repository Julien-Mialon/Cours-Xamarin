using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Common.Core.Database;
using Common.SqlMigrations.Models;
using ServiceStack.OrmLite;

namespace Common.SqlMigrations
{
	public class MigrationEngine
	{
		private readonly IDatabaseConnectionFactory _databaseFactory;
		private readonly List<IMigrationModule> _modules;

		public MigrationEngine(IDatabaseConnectionFactory databaseFactory, List<IMigrationModule> modules)
		{
			_databaseFactory = databaseFactory;
			_modules = modules;
		}

		public async Task<bool> Run(bool revert, string revertModuleName)
		{
			using (IDatabaseService databaseService = new DatabaseService(_databaseFactory))
			{
				IDbConnection connection = await databaseService.Connection;
	            
                connection.CreateTableIfNotExists<Migration>();

				Func<IMigrationOperation, IDbConnection, Task> applyMethod;
				Func<IEnumerable<IMigrationOperation>, int, IEnumerable<IMigrationOperation>> ordererMethod;
				if (revert)
				{
					ordererMethod = (source, lastAppliedMigration) => source.OrderByDescending(x => x.Number)
						.SkipWhile(x => x.Number > lastAppliedMigration).Take(1);
					applyMethod = (migration, db) => migration.Revert(db);
				}
				else
				{
					ordererMethod = (source, lastAppliedMigration) => source.OrderBy(x => x.Number).SkipWhile(x => x.Number <= lastAppliedMigration);
					applyMethod = (migration, db) => migration.Apply(db);
				}
				
				foreach (IMigrationModule module in _modules)
				{
					if (revert && module.Name != revertModuleName)
					{
						continue;
					}
					
					int lastAppliedMigration = await GetLastAppliedMigration(connection, module.Name) ?? -1;
					await module.StartMigrationOnModule(connection);
					foreach (IMigrationOperation operation in ordererMethod(module.Operations, lastAppliedMigration))
					{
						IDbTransaction transaction = connection.OpenTransaction();
						try
						{

							await applyMethod(operation, connection);

							if (revert)
							{
								await connection.DeleteAsync(connection.From<Migration>().Where(x => x.Module == module.Name && x.Number == operation.Number));
							}
							else
							{
								await connection.InsertAsync(new Migration
								{
									Module = module.Name,
									Number = operation.Number,
									AppliedDate = DateTime.UtcNow
								});
							}
							
							transaction.Commit();
						}
						catch (Exception exception)
						{
							Console.WriteLine(exception);
							transaction.Rollback();
							return false;
						}
						finally
						{
							transaction.Dispose();
						}
					}
					await module.EndMigrationOnModule(connection);
				}
            }

			return true;
		}
		
		private static async Task<int?> GetLastAppliedMigration(IDbConnection connection, string moduleName)
		{
			return (await connection.SingleAsync(
				connection.From<Migration>()
					.Where(x => x.Module == moduleName)
					.OrderByDescending(x => x.Number)
				)
			)?.Number;
		}
	}
}