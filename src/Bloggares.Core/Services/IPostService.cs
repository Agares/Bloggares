using System.Collections.Generic;
using Bloggares.Core.Entities;

namespace Bloggares.Core.Services
{
	public interface IPostService
	{
		IEnumerable<Post> All(long maxAccessLevel);

		Post BySlug(string slug, long maxAccessLevel);
	}
}