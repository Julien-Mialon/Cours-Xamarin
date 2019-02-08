using System.Threading.Tasks;

namespace Common.Core.CQRS
{
	public interface IQuery<in TParameter, TOutput>
	{
		Task<TOutput> Execute(TParameter parameter);
	}
}