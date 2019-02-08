using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace Common.Core.Extensions
{
	public static class StringExtensions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsNullOrEmpty(this string s) => string.IsNullOrEmpty(s);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool NotNullOrEmpty(this string s) => !string.IsNullOrEmpty(s);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AssertMandatory(this string s) => s == null || s != string.Empty;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string ValueIfNull(this string s, string value) => s ?? value;
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int? ValueIfNull(this int? s, int? value) => s ?? value;
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double? ValueIfNull(this double? s, double? value) => s ?? value;
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DateTime? ValueIfNull(this DateTime? s, DateTime? value) => s ?? value;
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static decimal? ValueIfNull(this decimal? s, decimal? value) => s ?? value;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string NullIfEmpty(this string source) => string.IsNullOrEmpty(source) ? null : source;
		
		public static string AsSha256(this string input)
		{
			using (SHA256 algorithm = SHA256.Create())
			{
				byte[] hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
				StringBuilder formatted = new StringBuilder(2 * hash.Length);
				foreach (byte b in hash)
				{
					formatted.AppendFormat("{0:X2}", b);
				}

				return formatted.ToString();
			}
		}
	}
}