using System;
using ServiceStack.DataAnnotations;

namespace Common.Core.Models
{
	public abstract class BaseDateTrackingEntity
	{
		[Index]
		public DateTime EntityCreatedDate { get; set; }
		
		public DateTime? EntityUpdatedDate { get; set; }
	}
}