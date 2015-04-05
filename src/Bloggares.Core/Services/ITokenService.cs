using System;

namespace Bloggares.Core.Services
{
	public interface ITokenService
	{
		Guid CreateTokenForUser(string username);
	}
}