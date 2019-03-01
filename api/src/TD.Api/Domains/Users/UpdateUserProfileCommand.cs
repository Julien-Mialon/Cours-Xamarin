using System;
using System.Threading.Tasks;
using Common.Core.Exceptions;
using Common.Core.Extensions;
using Common.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using TD.Api.Bases;
using TD.Api.Dtos;
using TD.Api.Services;

namespace TD.Api.Domains.Users
{
	public class UpdateUserProfileCommand : BaseAuthenticatedTdCommand<UpdateProfileRequest, UserItem>
	{
		public UpdateUserProfileCommand(IServiceProvider provider) : base(provider)
		{
		}

		protected override bool ValidateParameter(UpdateProfileRequest parameter)
		{
			return base.ValidateParameter(parameter) && 
				parameter.LastName.NotNullOrEmpty() && 
				parameter.FirstName.NotNullOrEmpty();
		}

		protected override async Task<UserItem> Action(UpdateProfileRequest parameter)
		{
			if (parameter.ImageId.HasValue)
			{
				IImageService imageService = Services.GetService<IImageService>();
				if (!await imageService.ImageExists(parameter.ImageId.Value))
				{
					throw new DomainException(Errors.IMAGE_NOT_FOUND, "Image id does not exists");
				}

				User.ImageId = parameter.ImageId.Value;
			}

			User.FirstName = parameter.FirstName;
			User.LastName = parameter.LastName;

			await Services.GetService<IUserService>().UpdateUser(User);

			return User.ToUserItem();
		}
	}
}