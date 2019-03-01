using System;
using System.Threading.Tasks;
using Common.Core;
using Common.Core.Extensions;
using Common.Core.Models;
using TD.Api.Bases;
using TD.Api.Dtos;

namespace TD.Api.Domains.Users
{
	public class GetUserProfileQuery : BaseAuthenticatedTdQuery<Unit, UserItem>
	{
		public GetUserProfileQuery(IServiceProvider provider) : base(provider)
		{
		}

		protected override Task<UserItem> Query(Unit parameter)
		{
			return User.ToUserItem().AsTask();
		}
	}
}