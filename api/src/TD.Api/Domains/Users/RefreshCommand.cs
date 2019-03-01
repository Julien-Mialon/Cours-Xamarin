using System;
using System.Threading.Tasks;
using Common.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
using TD.Api.Bases;
using TD.Api.Dtos;
using TD.Api.Services;

namespace TD.Api.Domains.Users
{
	public class RefreshCommand : BaseTdCommand<RefreshRequest, LoginResult>
	{
		public RefreshCommand(IServiceProvider services) : base(services)
		{
		}

		protected override bool ValidateParameter(RefreshRequest parameter)
		{
			return base.ValidateParameter(parameter) &&
			       parameter.RefreshToken.NotNullOrEmpty();
		}

		protected override Task<LoginResult> Action(RefreshRequest parameter)
		{
			return Services.GetService<IUserService>().Refresh(parameter.RefreshToken);
		}
	}
}