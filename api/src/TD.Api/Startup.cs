using System.Collections.Generic;
using Common.Api;
using Common.Api.Swaggers;
using Common.Core.CQRS;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TD.Api.Services;

namespace TD.Api
{
    public class Startup : BaseStartup
    {
        protected override string ProjectName { get; } = "td";
        public override List<string> DocumentationFileNames { get; } = new List<string> { "TD.Api.xml" };
        public override List<SwaggerVersionDoc> SwaggerDocuments { get; } = new List<SwaggerVersionDoc>
        {
            new SwaggerVersionDoc("TD", "v1", new SwaggerVersionModule[] { })
        };
        
        public Startup(IConfiguration configuration, IHostingEnvironment environment) : base(configuration, environment)
        {
        }

        protected override void OnConfigureServices(IServiceCollection services)
        {
            base.OnConfigureServices(services);

            services
                .AddScoped<IPlaceService, PlaceService>()
                .AddScoped<IImageService, ImageService>()
                .AddScoped<ICommentService, CommentService>()
                ;
        }
    }
}
