using System;
using System.Net;
using System.Threading.Tasks;
using Common.Core.Database;
using Common.Core.Exceptions;
using Common.Core.Extensions;
using ServiceStack.OrmLite;
using TD.Api.Dtos;
using TD.Api.Models;

namespace TD.Api.Services
{
	public interface IUserService
	{
		Task<LoginResult> Login(string email, string password);
		Task<LoginResult> Refresh(string refreshToken);
		Task<LoginResult> CreateAuthentication(User user);
		Task<User> Register(RegisterRequest data);
		Task UpdateUser(User user);
	}

	public class UserService : IUserService
	{
		private readonly IDatabaseService _databaseService;

		public UserService(IDatabaseService databaseService)
		{
			_databaseService = databaseService;
		}

		public async Task<LoginResult> Login(string email, string password)
		{
			var connection = await _databaseService.Connection;

			User user = await connection.From<User>()
				.Where(x => x.Email == email && x.Password == password)
				.NotDeleted()
				.AsSingleAsync(connection);

			if (user is null)
			{
				throw new DomainHttpCodeException(HttpStatusCode.Unauthorized, "Invalid credentials");
			}
			
			return await CreateAuthentication(user);
		}

		public async Task<LoginResult> Refresh(string refreshToken)
		{
			var connection = await _databaseService.Connection;

			AuthenticationToken refresh = await connection.From<AuthenticationToken>()
				.Where(x => x.RefreshToken == refreshToken)
				.NotDeleted()
				.AsSingleAsync(connection);

			if (refresh is null)
			{
				throw new DomainHttpCodeException(HttpStatusCode.Unauthorized, "Invalid refresh token");
			}

			User user = await connection.From<User>()
				.Where(x => x.Id == refresh.UserId)
				.NotDeleted()
				.AsSingleAsync(connection);

			if (user.IsDeleted)
			{
				throw new DomainHttpCodeException(HttpStatusCode.Forbidden, "User has been deleted");
			}

			refresh.IsDeleted = true;
			await connection.UpdateAsync(refresh);

			return await CreateAuthentication(user);
		}

		public async Task<LoginResult> CreateAuthentication(User user)
		{
			var connection = await _databaseService.Connection;
			
			AuthenticationToken token = new AuthenticationToken
			{
				UserId = user.Id,
				AccessToken = Guid.NewGuid().ToString("N"),
				RefreshToken = Guid.NewGuid().ToString("N"),
				ExpirationDate = DateTime.UtcNow.AddHours(1),
			};

			await connection.InsertAsync(token);
			
			return new LoginResult
			{
				AccessToken = token.AccessToken,
				RefreshToken =  token.RefreshToken,
				ExpiresIn = 3599,
				TokenType = "Bearer"
			};
		}

		public async Task<User> Register(RegisterRequest data)
		{
			var connection = await _databaseService.Connection;

			var existingEmail = await connection.From<User>()
				.Where(x => x.Email == data.Email)
				.NotDeleted()
				.AsSingleAsync(connection);

			if (existingEmail != null)
			{
				throw new DomainException(Errors.EMAIL_ALREADY_EXISTS, "Email already exists");
			}
			
			User result = new User
			{
				Email = data.Email,
				Password = data.Password,
				LastName = data.LastName,
				FirstName = data.FirstName,
				ImageId = null,
			};

			result.Id = (int) await connection.InsertAsync(result, selectIdentity: true);

			return result;
		}

		public async Task UpdateUser(User user)
		{
			var connection = await _databaseService.Connection;

			await connection.UpdateAsync(user);
		}
	}
}