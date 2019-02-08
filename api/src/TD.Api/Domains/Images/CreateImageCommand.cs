using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using TD.Api.Bases;
using TD.Api.Dtos;
using TD.Api.Services;

namespace TD.Api.Domains.Images
{
	public class CreateImageCommand : BaseTdCommand<IFormFile, ImageItem>
	{
		public CreateImageCommand(IServiceProvider provider) : base(provider)
		{
		}

		protected override async Task<ImageItem> Action(IFormFile parameter)
		{
			byte[] data;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (Stream inputStream = parameter.OpenReadStream())
				{
					await inputStream.CopyToAsync(memoryStream);
				}

				data = memoryStream.ToArray();
			}

			IImageService imageService = Services.GetService<IImageService>();
			
			return new ImageItem
			{
				Id = await imageService.CreateImage(data),
			};
		}
	}
}