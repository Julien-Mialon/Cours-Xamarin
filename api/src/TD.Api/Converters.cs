using TD.Api.Dtos;
using TD.Api.Models;

namespace TD.Api
{
	public static class Converters
	{
		public static UserItem ToUserItem(this User user)
		{
			if (user is null) return null;
			
			return new UserItem
			{
				Id = user.Id,
				Email = user.Email,
				ImageId = user.ImageId,
				LastName = user.LastName,
				FirstName = user.FirstName
			};
		}
	}
}