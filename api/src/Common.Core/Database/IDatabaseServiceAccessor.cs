using System.Data;
using System.Threading.Tasks;

namespace Common.Core.Database
{
	public interface IDatabaseServiceAccessor
	{
		Task<IDbConnection> Connection { get; }
	}
}