using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ServiceStack.OrmLite;

namespace Common.Core.Database
{
	public static class DatabaseExtensions
	{
		public static Task<List<T>> AsSelectAsync<T>(this SqlExpression<T> expression, IDbConnection connection)
		{
			return connection.SelectAsync(expression);
		}
		
		public static Task<List<T>> AsLoadSelectAsync<T>(this SqlExpression<T> expression, IDbConnection connection, params string[] fields)
		{
			return connection.LoadSelectAsync(expression, fields);
		}
		
		public static Task<T> AsSingleAsync<T>(this SqlExpression<T> expression, IDbConnection connection)
		{
			return connection.SingleAsync(expression);
		}
		
		public static async Task<T> AsLoadSingleAsync<T>(this SqlExpression<T> expression, IDbConnection connection, params string[] fields)
		{
			var result = await expression.Take(1).AsLoadSelectAsync(connection, fields);
			if (result.Count == 0)
			{
				return default(T);
			}

			return result[0];
		}
		
		public static async Task<int> AsCountAsync<T>(this SqlExpression<T> expression, IDbConnection connection)
		{
			return (int)await connection.CountAsync(expression);
		}
		
		public static async Task<List<T>> AsColumnAsync<T>(this ISqlExpression expression, IDbConnection connection)
		{
			return await connection.ColumnAsync<T>(expression);
		}

		public static string TableName(this Type type)
		{
			return type.GetModelMetadata().ModelName;
		}

		public static string ToSqlInRightOperand(this IEnumerable<Guid> ids)
		{
			return $"({string.Join(", ", ids.Select(x => $"'{x}'"))})";
		}

		public static (string SqlInRightOperand, List<IDbDataParameter> Parameters) ToParameterizedSqlIn<T>(this IEnumerable<T> source, string parameterPrefix, Func<(string ParameterName, T Item), IDbDataParameter> createParam)
		{
			int i = 0;
			List<IDbDataParameter> parameters = new List<IDbDataParameter>();
			foreach(var item in source)
			{
				parameters.Add(createParam(($"{parameterPrefix}_{i++}", item)));
			};
			return (SqlInRightOperand: String.Join(",", parameters.Select(p => p.ParameterName)), Parameters: parameters);
		}

		

		public static string SqlLimitString(this IDbConnection connection, int? offset = null, int? count = null)
		{
			return connection.GetDialectProvider().SqlLimit(offset, count);
		}
	}
}