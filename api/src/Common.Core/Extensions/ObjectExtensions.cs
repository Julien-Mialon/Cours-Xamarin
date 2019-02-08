using System.Threading.Tasks;

namespace Common.Core.Extensions
{
	public static class ObjectExtensions
	{
		public static bool NotNull(this object o) => o != null;

		public static bool TrueIfNull(this object o) => o == null;
		public static bool TrueIfNotNull(this object o) => o != null;
		public static bool FalseIfNull(this object o) => o != null;
		public static bool FalseIfNotNull(this object o) => o == null;
		
		public static async Task<bool> TrueIfNull<T>(this Task<T> o) => (await o).TrueIfNull();
		public static async Task<bool> TrueIfNotNull<T>(this Task<T> o) => (await o).TrueIfNotNull();
		public static async Task<bool> FalseIfNull<T>(this Task<T> o) => (await o).FalseIfNull();
		public static async Task<bool> FalseIfNotNull<T>(this Task<T> o) => (await o).FalseIfNotNull();
	}
}