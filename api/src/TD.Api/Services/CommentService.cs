using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Core.Database;
using Common.Core.Extensions;
using ServiceStack.OrmLite;
using TD.Api.Models;

namespace TD.Api.Services
{
	public interface ICommentService
	{
		Task<List<Comment>> ListComments(int placeId);
		Task Create(Comment comment);
	}
	
	public class CommentService : ICommentService
	{
		private readonly IDatabaseService _databaseService;

		public CommentService(IDatabaseService databaseService)
		{
			_databaseService = databaseService;
		}
		
		public async Task<List<Comment>> ListComments(int placeId)
		{
			var connection = await _databaseService.Connection;

			return await connection.From<Comment>()
				.NotDeleted()
				.Where(x => x.PlaceId == placeId)
				.OrderByDescending(x => x.EntityCreatedDate)
				.AsSelectAsync(connection);
		}

		public async Task Create(Comment comment)
		{
			var connection = await _databaseService.Connection;

			await connection.InsertAsync(comment);
		}
	}
}