using System.Net;
using Microsoft.AspNetCore.Mvc;
using Storm.Api.Controllers;
using Storm.Api.Dtos;
using Storm.Api.Swaggers.Attributes;
using TimeTracker.Api.Domains.Accounts.Actions;
using TimeTracker.Dtos;
using TimeTracker.Dtos.Accounts;
using TimeTracker.Dtos.Authentications;

namespace TimeTracker.Api.Controllers
{
	public class UserController : BaseController
	{
		public const string CATEGORY = "User";
		
		public UserController(IServiceProvider services) : base(services)
		{
		}

		[HttpGet]
		[Route(Urls.USER_PROFILE)]
		[Category(CATEGORY)]
		[Response(typeof(Response<UserProfileResponse>), HttpStatusCode.OK)]
		public async Task<IActionResult> UserProfile()
		{
			return await Action<UserProfileQuery, UserProfileQueryParameter, UserProfileResponse>(new UserProfileQueryParameter());
		}

		[HttpPatch]
		[Route(Urls.SET_USER_PROFILE)]
		[Category(CATEGORY)]
		[Response(typeof(Response<UserProfileResponse>), HttpStatusCode.OK)]
		public async Task<IActionResult> SetUserProfile([FromBody] SetUserProfileRequest request)
		{
			return await Action<SetUserProfileCommand, SetUserProfileCommandParameter, UserProfileResponse>(new SetUserProfileCommandParameter
			{
				Data = request
			});
		}
		
		[HttpPost]
		[Route(Urls.CREATE_USER)]
		[Category(CATEGORY)]
		[Response(typeof(Response<LoginResponse>), HttpStatusCode.OK)]
		public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
		{
			return await Action<CreateUserCommand, CreateUserCommandParameter, LoginResponse>(new CreateUserCommandParameter
			{
				Data = request
			});
		}
	}
}