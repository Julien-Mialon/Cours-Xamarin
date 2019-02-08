using System.Threading.Tasks;

namespace Common.Core.CQRS
{
	public interface ICommand<in TParameter, TOutput>
	{
		Task<TOutput> Execute(TParameter parameter);
	}
}