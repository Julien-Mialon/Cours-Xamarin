using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Common.Core.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using TD.Api.Bases;
using TD.Api.Dtos;
using TD.Api.Models;
using TD.Api.Services;

namespace TD.Api.Domains.Places
{
	public class GetPlaceQuery : BaseTdQuery<int, PlaceItem>
	{
		public GetPlaceQuery(IServiceProvider provider) : base(provider)
		{
		}

		protected override bool ValidateParameter(int id)
		{
			return id > 0;
		}

		protected override async Task<PlaceItem> Query(int id)
		{
			Place place = await Services.GetService<IPlaceService>().Get(id);

			if (place is null)
			{
				throw new DomainHttpCodeException(HttpStatusCode.NotFound, "This id does not exists");
			}

			List<Comment> comments = await Services.GetService<ICommentService>().ListComments(place.Id);
			
			return new PlaceItem
			{
				Id = place.Id,
				Latitude = place.Latitude,
				Longitude = place.Longitude,
				Description = place.Description,
				ImageId = place.ImageId,
				Title = place.Title,
				Comments = comments.ConvertAll(x => new CommentItem
				{
					Date = x.EntityCreatedDate,
					Text = x.Text,
					AuthorName = x.AuthorName
				})
			};
		}
	}
}