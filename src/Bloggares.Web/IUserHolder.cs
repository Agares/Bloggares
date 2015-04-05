using Bloggares.Common.Entities;
using Bloggares.Common.Result;

namespace Bloggares.Web
{
	public interface IUserHolder
	{
		Result<AuthorizedUser> User { get; }

		void SetUser(AuthorizedUser authorizedUser);
	}
}