using System;
using System.Threading.Tasks;
using Common.Core.Models;
using ServiceStack.OrmLite;

namespace Common.Core.Extensions
{
	public static class BaseEntityExtensions
	{
		public static SqlExpression<TBaseEntity> NotDeleted<TBaseEntity>(this SqlExpression<TBaseEntity> expression) where TBaseEntity : BaseEntity
		{
			return expression.Where(baseEntity => !baseEntity.IsDeleted);
		}

		public static void MarkCreate(this BaseDateTrackingEntity entity)
		{
			entity.EntityCreatedDate = DateTime.UtcNow;
		}

		public static void MarkUpdate(this BaseDateTrackingEntity entity)
		{
			entity.EntityUpdatedDate = DateTime.UtcNow;
		}

		public static void MarkDelete(this BaseEntity entity)
		{
			entity.IsDeleted = true;
			entity.EntityDeletedDate = DateTime.UtcNow;
		}

		public static T NullIfDeleted<T>(this T entity) where T : BaseEntity
		{
			if (entity.IsDeleted)
			{
				return null;
			}

			return entity;
		}
		
		public static async Task<T> NullIfDeleted<T>(this Task<T> entity) where T : BaseEntity
		{
			return (await entity).NullIfDeleted();
		}
	}


}