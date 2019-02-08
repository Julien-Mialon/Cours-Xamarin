using System;

namespace Common.Core.Database
{
	public interface IDatabaseTransaction : IDisposable
	{
		void Commit();

		void Rollback();
	}
}