using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ServiceStack.OrmLite.Dapper;

namespace Common.Core.Extensions
{
	public static class ManyToOneBinder<TKey, TPrimary, TValue>
	{
		public static async Task<List<TValue>> Execute<T2>(IDbConnection connection, string sql, Func<TPrimary, TKey> getKey, Func<TPrimary, TValue> turnToValue, Action<TValue, T2> bind)
		{
			Dictionary<TKey, TValue> results = new Dictionary<TKey, TValue>();
			List<TKey> order = new List<TKey>();

			await connection.QueryAsync<TPrimary, T2, bool>(sql, (primaryItem, related1) =>
			{
				TKey key = getKey(primaryItem);

				if (!results.TryGetValue(key, out TValue existingItem))
				{
					existingItem = turnToValue(primaryItem);
					results.Add(key, existingItem);
					order.Add(key);
				}

				if (related1 != null)
				{
					bind(existingItem, related1);
				}

				return true;
			});

			return order.Select(x => results[x]).ToList();
		}
	}
}