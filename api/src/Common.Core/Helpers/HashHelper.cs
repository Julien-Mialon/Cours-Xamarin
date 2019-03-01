using System.Security.Cryptography;
using System.Text;

namespace Common.Core.Helpers
{
	public static class HashHelper
	{
		public static string Hash(string input)
		{
			using (SHA256 algorithm = SHA256.Create())
			{
				byte[] hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
				
				StringBuilder result = new StringBuilder();

				for (var i = 0; i < hash.Length; i++)
				{
					result.Append($"{hash[i]:X2}");
				}
				
				return result.ToString();
			}
		}
	}
}