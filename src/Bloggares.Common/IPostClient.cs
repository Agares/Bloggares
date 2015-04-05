using System.Collections.Generic;
using Bloggares.Common.Entities;

namespace Bloggares.Common
{
	public interface IPostClient
	{
		IEnumerable<Post> All(AuthorizedUser user);

		Post Single(string slug, AuthorizedUser user);
	}
}