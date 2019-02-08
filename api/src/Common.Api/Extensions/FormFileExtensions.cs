using System.IO;
using System.Threading.Tasks;
using Common.Core.Parameters;
using Microsoft.AspNetCore.Http;

namespace Common.Api.Extensions
{
	public static class FormFileExtensions
	{
		public static async Task<FileParameter> ToFileParameter(this IFormFile file)
		{
			if (file == null || file.Length == 0)
			{
				return null;
			}
			
			MemoryStream memoryStream = new MemoryStream((int)file.Length);
			await file.CopyToAsync(memoryStream);
			memoryStream.Seek(0, SeekOrigin.Begin);
			
			return new FileParameter
			{
				ContentDisposition = file.ContentDisposition,
				ContentType = file.ContentType,
				FileName = file.FileName,
				Length = file.Length,
				Name = file.Name,
				InputStream = memoryStream,
			};
		}
	}
}