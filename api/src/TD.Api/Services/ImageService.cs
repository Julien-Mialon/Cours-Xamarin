using System;
using System.Data;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Common.Core.Database;
using Common.Core.Exceptions;
using ServiceStack.OrmLite;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using TD.Api.Models;

namespace TD.Api.Services
{
	public interface IImageService
	{
		Task<int> CreateImage(byte[] data);

		Task<bool> ImageExists(int imageId);

		Task<byte[]> GetImage(int imageId);
	}

	public class ImageService : IImageService
	{
		private readonly IDatabaseService _databaseService;

		public ImageService(IDatabaseService databaseService)
		{
			_databaseService = databaseService;
		}

		public async Task<int> CreateImage(byte[] data)
		{
			IDbConnection connection = await _databaseService.Connection;

			data = ConvertToImage(data);

			if (data is null)
			{
				throw new DomainHttpCodeException(HttpStatusCode.BadRequest, "Invalid image format");
			}
			
			ImageModel image = new ImageModel
			{
				Data = data,
			};

			return (int) await connection.InsertAsync(image, selectIdentity: true);
		}

		public async Task<bool> ImageExists(int imageId)
		{
			IDbConnection connection = await _databaseService.Connection;

			return await connection.SingleByIdAsync<ImageModel>(imageId) != null;
		}

		public async Task<byte[]> GetImage(int imageId)
		{
			IDbConnection connection = await _databaseService.Connection;

			ImageModel result = await connection.SingleByIdAsync<ImageModel>(imageId);

			return result?.Data;
		}

		private byte[] ConvertToImage(byte[] input)
		{
			try
			{
				using (var image = Image.Load(new MemoryStream(input)))
				using (var outputStream = new MemoryStream())
				{
					image.SaveAsJpeg(outputStream, new JpegEncoder {Quality = 95});
					return outputStream.ToArray();
				}
			}
			catch (Exception ex)
			{
				return null;
			}
		}
	}
}