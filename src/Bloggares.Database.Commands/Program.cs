using System.IO;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.Runtime;
using Npgsql;

namespace Bloggares.Database.Commands
{
	public class Program
	{
		private IApplicationEnvironment environment;
		private MigrationController migrationController;

		public Program(IApplicationEnvironment environment)
		{
			this.environment = environment;
		}

		// todo support downgrade
		// todo support (up|down)grade to any version
		public int Main(string[] arguments)
		{
			var config = new Configuration();
			config.AddCommandLine(arguments);
			if (File.Exists("config.json"))	// todo create some ConfigLoader or something
			{
				config.AddJsonFile("config.json");
			}

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