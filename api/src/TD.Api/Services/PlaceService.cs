using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Core.Database;
using Common.Core.Extensions;
using ServiceStack.OrmLite;
using TD.Api.Models;

namespace TD.Api.Services
{
	public interface IPlaceService
	{
		Task<List<Place>> ListPlaces();
		Task Create(Place place);
		Task<Place> Get(int placeId);
	}
	
	public class PlaceService : IPlaceService
	{
		private readonly IDatabaseService _databaseService;

		public PlaceService(IDatabaseService databaseService)
		{
			_databaseService = databaseService;
		}
		
		public async Task<List<Place>> ListPlaces()
		{
			var connection = await _databaseService.Connection;

			return await connection.From<Place>()
				.NotDeleted()
				.AsSelectAsync(connection);
		}

		public async Task Create(Place place)
		{
			var connection = await _databaseService.Connection;

			await connection.InsertAsync(place);
		}

		public async Task<Place> Get(int placeId)
		{
			var connection = await _databaseService.Connection;
			return await connection.From<Place>()
				.Where(x => x.Id == placeId)
				.NotDeleted()
				.AsSingleAsync(connection);
		}
	}
}