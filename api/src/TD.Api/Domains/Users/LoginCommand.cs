using System;
using System.Threading.Tasks;
using Common.Core.Extensions;
using Common.Core.Helpers;
using Microsoft.Extensions.DependencyInjection;
using TD.Api.Bases;
using TD.Api.Dtos;
using TD.Api.Services;

namespace TD.Api.Domains.Users
{
	public class LoginCommand : BaseTdCommand<LoginRequest, LoginResult>
	{
		public LoginCommand(IServiceProvider services) : base(services)
		{
		}

		protected override bool ValidateParameter(LoginRequest parameter)
		{
			return base.ValidateParameter(parameter) && 
				parameter.Email.NotNullOrEmpty() && 
				parameter.Password.NotNullOrEmpty();
		}

		protected override void PrepareParameters(LoginRequest parameter)
		{
			base.PrepareParameters(parameter);

			parameter.Email = parameter.Email.ToLowerInvariant();
			parameter.Password = HashHelper.Hash(parameter.Password);
		}

		protected override Task<LoginResult> Action(LoginRequest parameter)
		{
			return Services.GetService<IUserService>().Login(parameter.Email, parameter.Password);
		}
	}
}