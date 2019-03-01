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
		Task<List<(Comment comment, User user)>> ListComments(int placeId);
		Task Create(Comment comment);
	}
	
	public class CommentService : ICommentService
	{
		private readonly IDatabaseService _databaseService;

		public CommentService(IDatabaseService databaseService)
		{
			_databaseService = databaseService;
		}
		
		public async Task<List<(Comment comment, User user)>> ListComments(int placeId)
		{
			var connection = await _databaseService.Connection;

			var result = await connection.SelectMultiAsync<Comment, User>(connection.From<Comment>()
				.LeftJoin<Comment, User>(((comment, user) => comment.AuthorId == user.Id))
				.NotDeleted()
				.Where(x => x.PlaceId == placeId)
				.OrderByDescending(x => x.EntityCreatedDate)
			);

			return result.ConvertAll(x => (x.Item1, x.Item2));
		}

		public async Task Create(Comment comment)
		{
			var connection = await _databaseService.Connection;

			await connection.InsertAsync(comment);
		}
	}
}