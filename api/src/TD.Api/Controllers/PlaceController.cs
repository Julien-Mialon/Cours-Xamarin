using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Common.Api.Attributes;
using Common.Api.Controllers;
using Common.Api.Dtos;
using Common.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TD.Api.Domains.Comments;
using TD.Api.Domains.Images;
using TD.Api.Domains.Places;
using TD.Api.Dtos;

namespace TD.Api.Controllers
{
	[Controller]
	public class PlaceController : BaseController
	{
		public PlaceController(IServiceProvider services) : base(services)
		{
		}

		[HttpGet]
		[Route(Urls.LIST_PLACES)]
		[Response(typeof(Response<List<PlaceItemSummary>>), HttpStatusCode.OK)]
		public Task<IActionResult> ListPlaces()
		{
			return Query<ListPlacesQuery, Unit, List<PlaceItemSummary>>(Unit.Default);
		}

		[HttpGet]
		[Route(Urls.GET_PLACE)]
		[Response(typeof(Response<PlaceItem>), HttpStatusCode.OK)]
		[Response(typeof(Response), HttpStatusCode.BadRequest)]
		[Response(typeof(Response), HttpStatusCode.NotFound)]
		public Task<IActionResult> GetPlace([FromRoute] int placeId)
		{
			return Query<GetPlaceQuery, int, PlaceItem>(placeId);
		}

		[HttpPost]
		[Route(Urls.CREATE_PLACE)]
		[Response(typeof(Response), HttpStatusCode.OK)]
		[Response(typeof(Response), HttpStatusCode.BadRequest)]
		public Task<IActionResult> CreatePlace([FromBody] CreatePlaceRequest request)
		{
			return Command<CreatePlaceCommand, CreatePlaceRequest, Unit>(request);
		}
		
		[HttpPost]
		[Route(Urls.CREATE_COMMENT)]
		[Response(typeof(Response), HttpStatusCode.OK)]
		[Response(typeof(Response), HttpStatusCode.BadRequest)]
		[Response(typeof(Response), HttpStatusCode.NotFound)]
		public Task<IActionResult> CreateComment([FromRoute] int placeId, [FromBody] CreateCommentRequest data)
		{
			return Command<CreateCommentCommand, CreateCommentCommandParameter, Unit>(new CreateCommentCommandParameter
			{
				PlaceId = placeId,
				Request = data
			});
		}
		
		[HttpPost]
		[Route(Urls.CREATE_IMAGE)]
		[Response(typeof(Response<ImageItem>), HttpStatusCode.OK)]
		[Response(typeof(Response), HttpStatusCode.BadRequest)]
		[Response(typeof(Response), HttpStatusCode.NotFound)]
		public Task<IActionResult> CreateImage(IFormFile file)
		{
			return Command<CreateImageCommand, IFormFile, ImageItem>(file);
		}
		
		[HttpGet]
		[Route(Urls.GET_IMAGE)]
		[Response(typeof(byte[]), HttpStatusCode.OK)]
		[Response(typeof(Response), HttpStatusCode.BadRequest)]
		[Response(typeof(Response), HttpStatusCode.NotFound)]
		public Task<IActionResult> GetImage([FromRoute] int imageId)
		{
			return FileQuery<GetImageQuery, int>(imageId);
		}
	}
}