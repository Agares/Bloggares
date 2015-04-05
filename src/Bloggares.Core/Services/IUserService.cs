using System;
using Bloggares.Common.Entities;
using Bloggares.Common.Result;

namespace Bloggares.Core.Services
{
	public interface IUserService
	{
		Result<AuthorizedUser> GetUserByToken(Guid token);

		Result<AuthorizedUser> Authorize(string username, string password);
	}
}