using Bloggares.Core.Entities;

namespace Bloggares.Controllers
{
	public interface IUserAwareController
	{
		AuthorizedUser CurrentUser { set; }
	}
}