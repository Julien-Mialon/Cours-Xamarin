using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Core;
using Microsoft.Extensions.DependencyInjection;
using TD.Api.Bases;
using TD.Api.Dtos;
using TD.Api.Models;
using TD.Api.Services;

namespace TD.Api.Domains.Places
{
	public class ListPlacesQuery : BaseTdQuery<Unit, List<PlaceItemSummary>>
	{
		public ListPlacesQuery(IServiceProvider provider) : base(provider)
		{
		}

		protected override async Task<List<PlaceItemSummary>> Query(Unit _)
		{
			List<Place> places = await Services.GetService<IPlaceService>().ListPlaces();

			return places.ConvertAll(x => new PlaceItemSummary
			{
				Id = x.Id,
				Latitude = x.Latitude,
				Longitude = x.Longitude,
				Description = x.Description,
				ImageId = x.ImageId,
				Title = x.Title,
			});
		}
	}
}