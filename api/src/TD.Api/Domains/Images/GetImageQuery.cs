using System;
using System.Net;
using System.Threading.Tasks;
using Common.Core.Exceptions;
using Common.Core.Parameters;
using Microsoft.Extensions.DependencyInjection;
using TD.Api.Bases;
using TD.Api.Services;

namespace TD.Api.Domains.Images
{
	public class GetImageQuery : BaseTdQuery<int, FileQueryResult>
	{
		public GetImageQuery(IServiceProvider provider) : base(provider)
		{
		}

		protected override async Task<FileQueryResult> Query(int imageId)
		{
			IImageService imageService = Services.GetService<IImageService>();

			byte[] data = await imageService.GetImage(imageId);

			if (data is null)
			{
				throw new DomainHttpCodeException(HttpStatusCode.NotFound, "image not found");
			}
			
			return FileQueryResult.From(data, FileQueryResult.CONTENT_TYPE_JPG, "image.jpg");
		}
	}
}