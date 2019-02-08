using System;
using System.Threading.Tasks;
using Common.Api.Controllers;
using Common.Core.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace TD.Api.Controllers
{
    [ApiController]
    public class WelcomeController : BaseController
    {
        public WelcomeController(IServiceProvider services) : base(services)
        {
        }

        [Route("")]
        [HttpGet]
        public Task<IActionResult> Welcome()
        {
            return Content("<p>Welcome to TD API <br/>Julien Mialon © 2018</p>", "text/html")
                .AsTask<IActionResult>();
        }
    }
}
