using System.Collections.Generic;
using Bloggares.Entities;

namespace Bloggares.Services
{
	public interface IPostService
	{
		IEnumerable<Post> All(long maxAccessLevel);

		Post BySlug(string slug, long maxAccessLevel);
	}
}