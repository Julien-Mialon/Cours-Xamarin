using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Common.Api.Attributes;
using Common.Api.Controllers;
using Common.Api.Dtos;
using Common.Core;
using Microsoft.AspNetCore.Mvc;
using TD.Api.Domains.Places;
using TD.Api.Domains.Users;
using TD.Api.Dtos;

namespace TD.Api.Controllers
{
	[Controller]
	public class UserController : BaseController
	{
		public UserController(IServiceProvider services) : base(services)
		{
		}
		
		[HttpPost]
		[Route(Urls.LOGIN)]
		[Response(typeof(Response<LoginResult>), HttpStatusCode.OK)]
		public Task<IActionResult> Login([FromBody] LoginRequest request)
		{
			return Command<LoginCommand, LoginRequest, LoginResult>(request);
		}
		
		[HttpPost]
		[Route(Urls.REFRESH)]
		[Response(typeof(Response<LoginResult>), HttpStatusCode.OK)]
		public Task<IActionResult> Refresh([FromBody] RefreshRequest request)
		{
			return Command<RefreshCommand, RefreshRequest, LoginResult>(request);
		}
		
		[HttpPost]
		[Route(Urls.REGISTER)]
		[Response(typeof(Response<LoginResult>), HttpStatusCode.OK)]
		public Task<IActionResult> Register([FromBody] RegisterRequest request)
		{
			return Command<RegisterCommand, RegisterRequest, LoginResult>(request);
		}
		
		[HttpGet]
		[Route(Urls.ME)]
		[Response(typeof(Response<UserItem>), HttpStatusCode.OK)]
		public Task<IActionResult> GetMe()
		{
			return Query<GetUserProfileQuery, Unit, UserItem>(Unit.Default);
		}
		
		[HttpPatch]
		[Route(Urls.UPDATE_PROFILE)]
		[Response(typeof(Response<UserItem>), HttpStatusCode.OK)]
		public Task<IActionResult> UpdateUserProfile([FromBody] UpdateProfileRequest request)
		{
			return Command<UpdateUserProfileCommand, UpdateProfileRequest, UserItem>(request);
		}
		
		[HttpPatch]
		[Route(Urls.UPDATE_PASSWORD)]
		[Response(typeof(Response), HttpStatusCode.OK)]
		public Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordRequest request)
		{
			return Command<UpdatePasswordCommand, UpdatePasswordRequest, Unit>(request);
		}
	}
}