using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using Dapper;

namespace Bloggares.Database
{
	public static class DbConnectionExtensions
	{
		public static bool TableExists(this IDbConnection connection, string tableName)
		{
			Contract.Requires(connection != null);
			Contract.Requires(!string.IsNullOrEmpty(tableName));

			return connection
				.Query<bool>(
					"SELECT EXISTS(SELECT 1 FROM information_schema.tables WHERE table_name=@TableName)",
					new { TableName = tableName }
				).Single();
		}
	}
}