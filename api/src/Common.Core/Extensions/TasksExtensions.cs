using System.Threading.Tasks;

namespace Common.Core.Extensions
{
	public static class TasksExtensions
	{
		public static Task<T> AsTask<T>(this T result)
		{
			return Task.FromResult<T>(result);
		}
	}
}