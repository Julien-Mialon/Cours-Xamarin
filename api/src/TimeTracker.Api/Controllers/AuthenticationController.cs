using System.Net;
using Microsoft.AspNetCore.Mvc;
using Storm.Api.Controllers;
using Storm.Api.Dtos;
using Storm.Api.Swaggers.Attributes;
using TimeTracker.Api.Domains.Authentications.Actions;
using TimeTracker.Api.Domains.Authentications.Actions.Credentials;
using TimeTracker.Dtos;
using TimeTracker.Dtos.Authentications;
using TimeTracker.Dtos.Authentications.Credentials;

namespace TimeTracker.Api.Controllers
{
	public class AuthenticationController : BaseController
	{
		public const string CATEGORY = "Authentication";
		
		public AuthenticationController(IServiceProvider services) : base(services)
		{
		}

		[HttpPost]
		[Route(Urls.REFRESH_TOKEN)]
		[Category(CATEGORY)]
		[Response(typeof(Response<LoginResponse>), HttpStatusCode.OK)]
		public async Task<IActionResult> RefreshToken([FromBody] RefreshRequest request)
		{
			return await Action<RefreshTokenCommand, RefreshTokenCommandParameter, LoginResponse>(new RefreshTokenCommandParameter
			{
				RefreshToken = request.RefreshToken,
				ClientId = request.ClientId,
				ClientSecret = request.ClientSecret
			});
		}

		[HttpPost]
		[Route(Urls.LOGIN)]
		[Category(CATEGORY)]
		[Response(typeof(Response<LoginResponse>), HttpStatusCode.OK)]
		public async Task<IActionResult> LoginWithCredentials([FromBody] LoginWithCredentialsRequest request)
		{
			return await Action<LoginWithCredentialsCommand, LoginWithCredentialsCommandParameter, LoginResponse>(new LoginWithCredentialsCommandParameter
			{
				Login = request.Login,
				Password = request.Password,
				ClientId = request.ClientId,
				ClientSecret = request.ClientSecret
			});
		}

		[HttpPatch]
		[Route(Urls.SET_PASSWORD)]
		[Category(CATEGORY)]
		[Response(typeof(Response), HttpStatusCode.OK)]
		public async Task<IActionResult> SetPassword([FromBody] SetPasswordRequest request)
		{
			return await Action<SetPasswordCommand, SetPasswordCommandParameter>(new SetPasswordCommandParameter
			{
				Data = request
			});
		}
	}
}