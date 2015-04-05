using Bloggares.Common.Entities;

namespace Bloggares.WebService
{
	public interface IUserAwareController
	{
		AuthorizedUser CurrentUser { set; }
	}
}