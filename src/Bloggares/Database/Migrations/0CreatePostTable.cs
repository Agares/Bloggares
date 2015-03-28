using System;
using System.Data;
using Dapper;

namespace Bloggares.Database.Migrations
{
	public class CreatePostTable : ResourceBasedMigration
	{
		public override int Id
		{
			get
			{
				return 0;
			}
		}
	}
}