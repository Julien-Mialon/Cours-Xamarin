using System;
using System.IO;

namespace Common.Core.Parameters
{
	public class FileParameter : IDisposable
	{
		public string ContentType { get; set; }

		public string ContentDisposition { get; set; }

		public long Length { get; set; }

		public string Name { get; set; }

		public string FileName { get; set; }
		
		public Stream InputStream { get; set; }

		public void Dispose()
		{
			InputStream?.Dispose();
		}

		public static FileParameter From(byte[] data)
		{
			return new FileParameter
			{
				FileName = "<Static From>",
				Name = "<Static From>",
				InputStream = new MemoryStream(data),
				Length = data.Length
			};
		}
	}
}