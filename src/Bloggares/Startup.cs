using System.Data;
using Bloggares.Core;
using Bloggares.Core.CommandExecutors;
using Bloggares.Core.CQRS;
using Bloggares.Core.Services;
using Bloggares.Core.Services.DAL;
using Bloggares.Database;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.ConfigurationModel;
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

				var configurationProvider = new ConfigurationProvider();
				serviceCollection.AddInstance<IConfiguration>(configurationProvider.Configuration);

				var connection = new NpgsqlConnection(configurationProvider.Configuration.Get("Database:ConnectionString"));
				connection.Open();

				serviceCollection.AddInstance<IDbConnection>(connection);

				serviceCollection.AddTransient<IUserDAL, UserDAL>();
				serviceCollection.AddTransient<ITokenDAL, TokenDAL>();
				serviceCollection.AddTransient<ICryptographyService, CryptographyService>();
				serviceCollection.AddTransient<ITokenService, TokenService>();
				serviceCollection.AddTransient<IMigrationController, MigrationController>();
				serviceCollection.AddTransient<IUserService, UserService>();
				serviceCollection.AddTransient<IPostService, PostService>();

				serviceCollection.AddTransient<ICommandExecutor, UserCreateCommandExecutor>();
				serviceCollection.AddTransient<ICommandManager, CommandManager>();
			});

			app.UseMvc();
		}
	}
}