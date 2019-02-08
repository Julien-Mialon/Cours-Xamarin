using System;

namespace Common.Core.Models
{
	public abstract class BaseEntity : BaseDateTrackingEntity
	{
		public bool IsDeleted { get; set; }
		
		public DateTime? EntityDeletedDate { get; set; }
	}
}