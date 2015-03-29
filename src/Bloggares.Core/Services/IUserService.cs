using System;
using Bloggares.Core.Entities;

namespace Bloggares.Core.Services
{
	public interface IUserService
	{
		Result<AuthorizedUser> GetUserByToken(Guid token);

		Result<AuthorizedUser> Authorize(string username, string password);
	}
}