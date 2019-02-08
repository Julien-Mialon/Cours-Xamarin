using System.Data;
using Common.Core.Extensions;
using Common.Core.Models;
using ServiceStack.OrmLite;

namespace Common.Core.Database
{
	public static class OrmLiteInterceptors
	{
		public static void Initialize()
		{
			OrmLiteConfig.InsertFilter = OnInsert;
			OrmLiteConfig.UpdateFilter = OnUpdate;
		}

		private static void OnInsert(IDbCommand command, object item)
		{
			if (item is BaseDateTrackingEntity entity)
			{
				entity.MarkCreate();
			}
		}
		
		private static void OnUpdate(IDbCommand command, object item)
		{
			if (item is BaseEntity entity)
			{
				if (entity.IsDeleted)
				{
					entity.MarkDelete();
				}
				else
				{
					entity.MarkUpdate();
				}
			}
			else if (item is BaseDateTrackingEntity trackingEntity)
			{
				trackingEntity.MarkUpdate();
			}
		}
	}
}