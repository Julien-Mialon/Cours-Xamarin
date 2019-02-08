using System;
using Common.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Core.Extensions
{
	public static class ServicesExtensions
	{
		public static DateTime CurrentDate(this IServiceProvider provider)
		{
			return provider.GetService<IDateService>().Now;
		}

		public static string TimestampedFileName(this IDateService dateService, string format)
		{
			return string.Format(format, dateService.Now.ToString("yyyy-MM-dd_HHmmss"));
		}
	}
}