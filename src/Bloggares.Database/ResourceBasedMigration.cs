using System;
using System.Data;
using System.Diagnostics.Contracts;
using System.IO;
using System.Reflection;
using Dapper;

namespace Bloggares.Database
{
	public abstract class ResourceBasedMigration : IMigration
	{
		private const string UpFilename = "Up.sql";
		private const string DownFilename = "Down.sql";

		private Assembly resourceAssembly;

		public abstract int Id { get; }

		public ResourceBasedMigration(Assembly resourceAssembly = null)
		{
			Contract.Ensures(this.resourceAssembly != null);

			if (resourceAssembly == null)
			{
				resourceAssembly = Assembly.GetCallingAssembly();
            }

			this.resourceAssembly = resourceAssembly;
		}

		public void Execute(IDbConnection connection)
		{
			Contract.Requires(connection != null);

			connection.Execute(ReadQueryFromResource(UpFilename));
		}

		public void Revert(IDbConnection connection)
		{
			Contract.Requires(connection != null);

			connection.Execute(ReadQueryFromResource(DownFilename));
		}

		// todo SRP violation, move to IQueryReader -> ResourceQueryReader
		private string ReadQueryFromResource(string filename)
		{
			Contract.Requires(!string.IsNullOrEmpty(filename));

			var queryStream = resourceAssembly
				.GetManifestResourceStream("DBMigrationScripts/" + Id + "/" + filename);
			var queryReader = new StreamReader(queryStream);
			var query = queryReader.ReadToEnd();

			return query;
		}
	}
}