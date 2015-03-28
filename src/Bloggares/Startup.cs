using System.Data;
using Bloggares.Core.Services;
using Bloggares.Core.Services.DAL;
using Bloggares.Database;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using Npgsql;

namespace Bloggares
{
	public class Startup
	{
		public void Configure(IApplicationBuilder app)
		{
			app.UseServices(serviceCollection =>
			{
				serviceCollection.AddMvc();

				// todo move to extension method
				// todo read Connection String from configuration
				var connection = new NpgsqlConnection("Server=127.0.0.1;Database=Bloggares;User Id=Bloggares;Password=Bloggares");
				connection.Open();
				serviceCollection.AddInstance<IDbConnection>(connection);

				// todo all services should have interfaces
				serviceCollection.AddTransient<UserDAL>();
				serviceCollection.AddTransient<TokenDAL>();
				serviceCollection.AddTransient<CryptographyService>();
				serviceCollection.AddTransient<TokenService>();
				serviceCollection.AddTransient<IMigrationController, MigrationController>();
				serviceCollection.AddTransient<IUserService, UserService>();
				serviceCollection.AddTransient<IPostService, PostService>();
			});

			app.UseMvc();
		}
	}
}