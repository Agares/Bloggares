using System;
using Bloggares.Common;
using Bloggares.Web.Controllers;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Logging.Console;
using RestSharp;

namespace Bloggares.Web
{
	public class Startup
	{
		private const string DevelopmentEnvironmentName = "Development";

		public IConfiguration Configuration { get; set; }

		public Startup(IHostingEnvironment env)
		{
			Configuration = new Configuration()
				.AddJsonFile("config.json")
				.AddEnvironmentVariables();
		}

		public void ConfigureServices(IServiceCollection services)
		{
			var baseUrl = Configuration.Get("WebService:Url");
			var restClient = new RestClient(baseUrl);

			var jsonDeserializer = new BloggaresJsonDeserializer();
			restClient.AddHandler("text/json", jsonDeserializer);
			restClient.AddHandler("application/json", jsonDeserializer);
			services.AddInstance<IRestClient>(restClient);

			services.AddCachingServices();
			services.AddSessionServices();

			services.AddTransient<IUserHolder, UserHolder>();
			services.AddTransient<IAuthenticationClient, AuthenticationClient>();
			services.AddTransient<IPostClient, PostClient>();

			services.AddMvc();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerfactory)
		{
			loggerfactory.AddConsole();

			if (string.Equals(env.EnvironmentName, DevelopmentEnvironmentName, StringComparison.OrdinalIgnoreCase))
			{
				app.UseBrowserLink();
				app.UseErrorPage(ErrorPageOptions.ShowAll);
			}
			else
			{
				// todo move to ErrorController, instead of this bullshit
				app.UseErrorHandler("/Home/Error");
			}

			app.UseStaticFiles();
			app.UseInMemorySession();
			app.UseMvc();
		}
	}
}