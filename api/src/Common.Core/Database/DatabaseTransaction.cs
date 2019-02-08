using System.Data;

namespace Common.Core.Database
{
	public class DatabaseTransaction : IDatabaseTransaction
	{
		private IDbTransaction _transaction;
		private bool _finalized;

		public DatabaseTransaction(IDbTransaction transaction)
		{
			_transaction = transaction;
		}

		public void Dispose()
		{
			if (!_finalized)
			{
				_finalized = true;
				_transaction.Commit();
			}
			_transaction.Dispose();
			_transaction = null;
		}

		public void Commit()
		{
			_finalized = true;
			_transaction.Commit();
		}

		public void Rollback()
		{
			_finalized = true;
			_transaction.Rollback();
		}
	}
}