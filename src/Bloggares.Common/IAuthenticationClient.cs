using System;
using Bloggares.Common.Entities;

namespace Bloggares.Common
{
	public interface IAuthenticationClient
	{
		AuthorizedUser Authenticate(string username, string password);
		AuthorizedUser GetUserByToken(Guid token);
	}
}