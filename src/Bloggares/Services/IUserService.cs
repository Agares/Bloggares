using System;
using Bloggares.Commands;
using Bloggares.Entities;

namespace Bloggares.Services
{
	public interface IUserService
	{
		// todo rename at least on of "Authorize" to something more meaningful
		AuthorizedUser Authorize(Guid token); // todo ValidateToken?

		AuthorizedUser Authorize(string username, string password);	// todo should be a command

		void Create(UserCreateCommand command);	// todo commands should probably be moved away from services
	}
}