namespace TD.Api
{
	public static class Urls
	{
		public const string LOGIN = "auth/login"; // POST login with email/password
		public const string REFRESH = "auth/refresh"; // POST refresh a token
		public const string REGISTER = "auth/register"; // POST register a user
		public const string ME = "me"; // GET User profile
		public const string UPDATE_PROFILE = "me"; // PATCH Update profile (firstname, lastname)
		public const string UPDATE_PASSWORD = "me/password"; // PATCH Update password
		
		public const string LIST_PLACES = "places"; // GET List of places
		public const string GET_PLACE = "places/{placeId}"; // GET place detail
		
		public const string CREATE_PLACE = "places"; //POST Create a place
		public const string CREATE_COMMENT = "places/{placeId}/comments"; //POST Add a comment

		public const string CREATE_IMAGE = "images"; //POST upload an image
		public const string GET_IMAGE = "images/{imageId}"; //GET retrieve image data
	}

	public static class Errors
	{
		public const string IMAGE_NOT_FOUND = nameof(IMAGE_NOT_FOUND);

		public const string EMAIL_ALREADY_EXISTS = nameof(EMAIL_ALREADY_EXISTS);
	}
}