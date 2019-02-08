namespace TD.Api
{
	public static class Urls
	{
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
	}
}