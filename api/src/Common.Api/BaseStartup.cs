using System.Collections.Generic;
using System.Globalization;
using Common.Api.Authentications;
using Common.Api.Configurations;
using Common.Api.Database;
using Common.Api.Swaggers;
using Common.Core.Database;
using Common.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Common.Api
{
	public abstract class BaseStartup
	{
		protected IConfiguration Configuration { get; }
		protected IHostingEnvironment Environment { get; }

		protected BaseStartup(IConfiguration configuration, IHostingEnvironment environment)
		{
			Environment = environment;
			Configuration = configuration;
		}

		protected abstract string ProjectName { get; }

		public abstract List<string> DocumentationFileNames { get; }

		public abstract List<SwaggerVersionDoc> SwaggerDocuments { get; }

		protected virtual string LogConfigurationSectionName => "ElasticSearch";

		protected virtual string DatabaseConfigurationSectionName => "Database";

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<IDateService, DateService>()
				.AddSingleton<ITokenAuthenticator, TokenAuthenticator>();

			//databases
			services.AddSingleton(Configuration.GetSection(DatabaseConfigurationSectionName).LoadDatabaseConfiguration().CreateFactory())
				.AddScoped<IDatabaseService, DatabaseService>()
				.AddSingleton<IDatabaseServiceAccessor, DatabaseServiceAccessor>();
			OrmLiteInterceptors.Initialize();

			// frameworks
			services.AddMvc()
				.AddJsonOptions(options => { options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local; })
				;
			services.AddCors();
			services.AddConfiguredSwagger(Environment, SwaggerDocuments, DocumentationFileNames);
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

			services.Configure<FormOptions>(x =>
			{
				x.ValueLengthLimit = 1024 * 1024 * 1024; //1GB
				x.MultipartBodyLengthLimit = 1024 * 1024 * 1024; //1GB
			});

			services.Configure<RequestLocalizationOptions>(options =>
			{
				CultureInfo[] supportedCultures = new[]
				{
					new CultureInfo("fr")
				};
				options.DefaultRequestCulture = new RequestCulture(culture: "fr", uiCulture: "fr");
				options.SupportedCultures = supportedCultures;
				options.SupportedUICultures = supportedCultures;
			});

			OnConfigureServices(services);
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (!EnvironmentHelper.IsAvailableClient)
			{
				app.UseDeveloperExceptionPage();
			}

			IOptions<RequestLocalizationOptions> options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
			
			app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod())
				.UseCommonSwagger(SwaggerDocuments)
				.UseDisposeConnectionMiddleware()
				.UseRequestLocalization(options.Value);

			OnConfigureApplication(app, env);

			app.UseMvc();
		}

		protected virtual void OnConfigureServices(IServiceCollection services)
		{
		}

		protected virtual void OnConfigureApplication(IApplicationBuilder app, IHostingEnvironment env)
		{
			
		}
	}
}