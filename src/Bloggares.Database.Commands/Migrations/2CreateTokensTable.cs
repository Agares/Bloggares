using System;
using System.Data;
using Dapper;

namespace Bloggares.Database.Migrations
{
	public class CreateTokensTable : ResourceBasedMigration
	{
		public override int Id
		{
			get
			{
				return 2;
			}
		}
	}
}