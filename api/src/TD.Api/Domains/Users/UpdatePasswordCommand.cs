using System;
using System.Net;
using System.Threading.Tasks;
using Common.Core;
using Common.Core.Exceptions;
using Common.Core.Extensions;
using Common.Core.Helpers;
using Common.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using TD.Api.Bases;
using TD.Api.Dtos;
using TD.Api.Services;

namespace TD.Api.Domains.Users
{
	public class UpdatePasswordCommand : BaseAuthenticatedTdCommand<UpdatePasswordRequest, Unit>
	{
		public UpdatePasswordCommand(IServiceProvider provider) : base(provider)
		{
		}

		protected override bool ValidateParameter(UpdatePasswordRequest parameter)
		{
			return base.ValidateParameter(parameter) &&
				parameter.OldPassword.NotNullOrEmpty() && 
				parameter.NewPassword.NotNullOrEmpty();
		}

		protected override void PrepareParameters(UpdatePasswordRequest parameter)
		{
			base.PrepareParameters(parameter);

			parameter.OldPassword = HashHelper.Hash(parameter.OldPassword);
			parameter.NewPassword = HashHelper.Hash(parameter.NewPassword);
		}

		protected override async Task<Unit> Action(UpdatePasswordRequest parameter)
		{
			if (User.Password != parameter.OldPassword)
			{
				throw new DomainHttpCodeException(HttpStatusCode.Forbidden, "Invalid password");
			}

			User.Password = parameter.NewPassword;
			await Services.GetService<IUserService>().UpdateUser(User);

			return Unit.Default;
		}
	}
}