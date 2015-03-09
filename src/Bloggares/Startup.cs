using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.DependencyInjection;

namespace Bloggares
{
	public class Startup
	{
		public void Configure(IApplicationBuilder app)
		{
			app.UseServices(serviceCollection => {
				serviceCollection.AddMvc();
				/*serviceCollection.Configure<MvcOptions>(x =>
				{
					x.Filters.Add(typeof(JsonNameTranslationFilter));
				});*/
			});

			app.UseMvc();
		}
	}
}