using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Core.Extensions
{
	public static class CollectionExtensions
	{
		public static List<T> NullIfEmpty<T>(this List<T> source)
		{
			if (source == null || source.Count == 0)
			{
				return null;
			}

			return source;
		}

		public static IReadOnlyList<T> EmptyIfNull<T>(this IReadOnlyList<T> source)
		{
			return source ?? EmptyList<T>.Singleton;
		}


		public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> source)
		{
			return source ?? Enumerable.Empty<T>();
		}

		public static bool NotNullOrEmpty<T>(this List<T> source)
		{
			return source?.Count > 0;
		}

		public static bool NotNullOrEmpty<T>(this T[] source)
		{
			return source?.Length > 0;
		}

		public static bool NotNullOrEmpty<T>(this IEnumerable<T> source)
		{
			return (source?.Any() ?? false);
		}

		public static bool IsNullOrEmpty<T>(this IReadOnlyCollection<T> source)
		{
			return source == null || source.Count == 0;
		}

		public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
		{
			return new HashSet<T>(source);
		}

		public static HashSet<TResult> ToHashSet<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
		{
			return source.Select(selector).ToHashSet();
		}

		public static async Task<TElement> FirstOrDefault<TElement>(this Task<IEnumerable<TElement>> source)
		{
			return (await source).FirstOrDefault();
		}

		public static async Task<TElement> FirstOrDefault<TElement>(this Task<List<TElement>> source)
		{
			return (await source).FirstOrDefault();
		}

		public static void Add<TKey, TKey2, TValue>(this Dictionary<TKey, Dictionary<TKey2, TValue>> items, TKey key, TKey2 key2, TValue value)
		{
			if (!items.TryGetValue(key, out var d))
			{
				items.Add(key, d = new Dictionary<TKey2, TValue>());
			}

			d[key2] = value;
		}

		public static bool TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> items, TKey key, TValue value)
		{
			if (items.ContainsKey(key))
			{
				return false;
			}

			items.Add(key, value);
			return true;
		}


		#region Nested types
		private static class EmptyList<T>
		{
			public static readonly ReadOnlyCollection<T> Singleton = new List<T>(0).AsReadOnly();
		}
		#endregion Nested types
	}
}