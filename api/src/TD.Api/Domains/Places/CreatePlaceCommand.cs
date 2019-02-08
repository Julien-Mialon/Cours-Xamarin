using System;
using System.Net;
using System.Threading.Tasks;
using Common.Core;
using Common.Core.CQRS;
using Common.Core.Exceptions;
using Common.Core.Extensions;
using Common.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using TD.Api.Dtos;
using TD.Api.Models;
using TD.Api.Services;

namespace TD.Api.Domains.Places
{
	public class CreatePlaceCommand : BaseCommand<CreatePlaceRequest, Unit>
	{
		public CreatePlaceCommand(IServiceProvider services) : base(services)
		{
		}

		protected override bool ValidateParameter(CreatePlaceRequest parameter)
		{
			return base.ValidateParameter(parameter) &&
			       parameter.Title.NotNullOrEmpty() &&
			       parameter.Description.NotNullOrEmpty() &&
			       parameter.ImageId > 0 &&
			       parameter.Latitude != 0 &&
			       parameter.Longitude != 0;
		}

		protected override async Task<Unit> Action(CreatePlaceRequest parameter)
		{
			IPlaceService service = Services.GetService<IPlaceService>();
			IImageService imageService = Services.GetService<IImageService>();

			if (!await imageService.ImageExists(parameter.ImageId))
			{
				throw new DomainException(Errors.IMAGE_NOT_FOUND, "Image id does not exists");
			}
			
			Place place = new Place
			{
				Title = parameter.Title,
				Description = parameter.Description,
				Latitude = parameter.Latitude,
				Longitude = parameter.Longitude,
				ImageId = parameter.ImageId,
			};

			await service.Create(place);

			return Unit.Default;
		}
	}
}