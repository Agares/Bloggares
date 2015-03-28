using Bloggares.Core.Commands;
using Bloggares.Core.Entities;

namespace Bloggares.Core.Services
{
	public interface IUserService
	{
		Result<AuthorizedUser> Authorize(string username, string password);	// todo should be a command

		void Create(UserCreateCommand command);	// todo commands should probably be moved away from services
	}
}