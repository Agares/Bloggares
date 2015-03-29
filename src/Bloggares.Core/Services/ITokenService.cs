using System;
using Bloggares.Core.Entities;

namespace Bloggares.Core.Services
{
	public interface ITokenService
	{
		Guid CreateTokenForUser(string username);

		Result<AuthorizedUser> GetUserByToken(Guid token);
	}
}