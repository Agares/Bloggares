using Bloggares.Core.Entities;

namespace Bloggares.Core.Services
{
	public interface ITokenService
	{
		Token CreateTokenForUser(string username);
	}
}