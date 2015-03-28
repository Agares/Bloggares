using System;
using System.Collections.Generic;
using System.Data;
using Dapper;

namespace Bloggares.Database
{
	public class MigrationDAO
	{
		private readonly IDbConnection connection;

		public MigrationDAO(IDbConnection connection)
		{
			this.connection = connection;
		}

		public void EnsureMigrationsTableExists()
		{
			if (!connection.TableExists("_migrations"))
			{
				connection.Execute(@"CREATE TABLE _migrations (id INT PRIMARY KEY, ""when"" TIMESTAMP WITH TIME ZONE)");
			}
		}

		public IEnumerable<int> FindAlreadyExecutedMigrationIds()
		{
			return connection.Query<int>(@"SELECT id FROM _migrations ORDER BY id");
		}

		public void MarkAsExecuted(int id)
		{
			connection.Execute(@"INSERT INTO _migrations(id, ""when"") VALUES(@id, @when)", new { id, when = DateTime.Now });
		}
	}
}