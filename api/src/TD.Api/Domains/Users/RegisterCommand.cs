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
	public class RegisterCommand : BaseTdCommand<RegisterRequest, LoginResult>
	{
		public RegisterCommand(IServiceProvider services) : base(services)
		{
		}

		protected override bool ValidateParameter(RegisterRequest parameter)
		{
			return base.ValidateParameter(parameter) && 
				parameter.Email.NotNullOrEmpty() && 
				parameter.Password.NotNullOrEmpty() && 
				parameter.FirstName.NotNullOrEmpty() && 
				parameter.LastName.NotNullOrEmpty();
		}

		protected override void PrepareParameters(RegisterRequest parameter)
		{
			base.PrepareParameters(parameter);

			parameter.Email = parameter.Email.ToLowerInvariant();
			parameter.Password = HashHelper.Hash(parameter.Password);
		}

		protected override async Task<LoginResult> Action(RegisterRequest parameter)
		{
			IUserService userService = Services.GetService<IUserService>();

			var user = await userService.Register(parameter);

			return await userService.CreateAuthentication(user);
		}
	}
}