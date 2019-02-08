namespace Common.Core.Parameters
{
	public class FileQueryResult
	{
		public const string CONTENT_TYPE_PDF = "application/pdf";
		public const string CONTENT_TYPE_PNG = "image/png";
		public const string CONTENT_TYPE_JPG = "image/jpeg";
		public const string CONTENT_TYPE_EXCEL = "application/octet-stream";
		
		public byte[] Data { get; set; }
		
		public string ContentType { get; set; }
		
		public string FileName { get; set; }

		public static FileQueryResult From(byte[] data, string contentType)
		{
			return new FileQueryResult
			{
				Data = data,
				ContentType = contentType
			};
		}
		
		public static FileQueryResult From(byte[] data, string contentType, string fileName)
		{
			return new FileQueryResult
			{
				Data = data,
				ContentType = contentType,
				FileName = fileName
			};
		}
	}
}