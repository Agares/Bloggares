using System;
using System.Collections.Generic;
using Bloggares.Core;
using Bloggares.Core.CommandExecutors;
using Bloggares.Core.Commands;
using Bloggares.Core.CQRS;
using Bloggares.Core.Services;
using Bloggares.Core.Services.DAL;
using Npgsql;

namespace Bloggares.Commands
{
	public class Program
	{
		public int Main(string[] args)
		{
			var configurationProvider = new ConfigurationProvider();
			var config = configurationProvider.Configuration;
			var connectionString = config.Get("Database:ConnectionString");
			var connection = new NpgsqlConnection(connectionString);
			connection.Open();

			if (args.Length == 0)
			{
				Console.WriteLine("No command choosen.");
				return 1;
			}

			switch (args[0])
			{
				case "user:create":
					if (args.Length != 4)
					{
						Console.WriteLine("Usage: {0} [username] [password] [accessLevel]", args[0]);
						Console.WriteLine("Invalid number of arguments.");
						return 2;
					}

					var userDAL = new UserDAL(connection);
					var cryptographyService = new CryptographyService();
					var commandManager = new CommandManager(new List<ICommandExecutor> { new UserCreateCommandExecutor(cryptographyService, userDAL) });

					commandManager.Execute(new UserCreateCommand(args[1], args[2], long.Parse(args[3])));
					break;

				default:
					Console.WriteLine("Unknown command.");
					return 3;
			}

			return 0;
		}
	}
}