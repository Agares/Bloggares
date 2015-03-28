using System.Collections.Generic;
using System.Data;
using System.Linq;
using Bloggares.Core.Entities;
using Dapper;

namespace Bloggares.Core.Services
{
	public class PostService : IPostService
	{
		private readonly IDbConnection connection;

		public PostService(IDbConnection connection)
		{
			this.connection = connection;
		}

		public IEnumerable<Post> All(long accessLevel)
		{
			return connection.Query<Post>(
				@"SELECT title, content, slug FROM posts WHERE accessLevel <= @AccessLevel",
				new { AccessLevel = accessLevel }
			);
		}

		public Post BySlug(string slug, long accessLevel)
		{
			return connection.Query<Post>(
				@"SELECT title, content, slug FROM posts WHERE Slug=@Slug AND accessLevel <= @AccessLevel",
				new { Slug = slug, AccessLevel = accessLevel }
			).Single();
		}
	}
}