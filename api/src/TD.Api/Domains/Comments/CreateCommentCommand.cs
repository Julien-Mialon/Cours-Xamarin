using System;
using System.Net;
using System.Threading.Tasks;
using Common.Core;
using Common.Core.Exceptions;
using Common.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
using TD.Api.Bases;
using TD.Api.Dtos;
using TD.Api.Models;
using TD.Api.Services;

namespace TD.Api.Domains.Comments
{
	public class CreateCommentCommandParameter
	{
		public int PlaceId { get; set; }

		public CreateCommentRequest Request { get; set; }
	}

	public class CreateCommentCommand : BaseTdCommand<CreateCommentCommandParameter, Unit>
	{
		public CreateCommentCommand(IServiceProvider provider) : base(provider)
		{
		}

		protected override bool ValidateParameter(CreateCommentCommandParameter parameter)
		{
			return base.ValidateParameter(parameter) &&
			       parameter.PlaceId > 0 &&
			       parameter.Request.NotNull() &&
			       parameter.Request.Text.NotNullOrEmpty() &&
			       parameter.Request.AuthorName.NotNullOrEmpty();
		}

		protected override async Task<Unit> Action(CreateCommentCommandParameter parameter)
		{
			IPlaceService placeService = Services.GetService<IPlaceService>();

			Place place = await placeService.Get(parameter.PlaceId);

			if (place is null)
			{
				throw new DomainHttpCodeException(HttpStatusCode.NotFound, "invalid place id");
			}

			ICommentService commentService = Services.GetService<ICommentService>();
			
			await commentService.Create(new Comment
			{
				Text = parameter.Request.Text,
				AuthorName = parameter.Request.AuthorName,
				PlaceId = place.Id
			});

			return Unit.Default;
		}
	}
}