using System;

namespace Common.Core.Services
{
	public interface IDateService
	{
		DateTime Now { get; }
	}
	
	public class DateService : IDateService
	{
		public DateTime Now => DateTime.UtcNow;
	}
}