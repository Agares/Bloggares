using Bloggares.Core;
using Npgsql;

namespace Bloggares.Database.Commands
{
	public class Program
	{
		// todo support downgrade
		// todo support (up|down)grade to any version
		public int Main(string[] arguments)
		{
			var configurationProvider = new ConfigurationProvider();
			var config = configurationProvider.Configuration;
			// todo use DI?
			var connectionString = config.Get("Database:ConnectionString");
			var connection = new NpgsqlConnection(connectionString);
			connection.Open(); // todo should MigrationController open it by itself? check what's the best practice here
			var migrationController = new MigrationController(connection, new MigrationDAO(connection), GetType().Assembly); // todo move migrations to own Assembly or what?
			migrationController.UpgradeDatabase();

			return 0;
		}
	}
}