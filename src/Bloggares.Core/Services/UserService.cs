using Bloggares.Core.Commands;
using Bloggares.Core.Entities;
using Bloggares.Core.Services.DAL;

namespace Bloggares.Core.Services
{
	public class UserService : IUserService
	{
		private readonly UserDAL userDAL;
		private readonly TokenService tokenService;
		private readonly CryptographyService cryptographyService;

		public UserService(UserDAL userDAL, TokenService tokenService, CryptographyService cryptographyService)
		{
			this.userDAL = userDAL;
			this.tokenService = tokenService;
			this.cryptographyService = cryptographyService;
		}

		public void Create(UserCreateCommand command)
		{
			var passwordHash = cryptographyService.HashPassword(command.Password);

			userDAL.Create(command.Username, passwordHash, command.AccessLevel);
		}

		// todo should be a command
		public Result<AuthorizedUser> Authorize(string username, string password) // todo UserCredentials object
		{
			var passwordHash = cryptographyService.HashPassword(password);
			var user = userDAL.FindUserByCredentials(username, passwordHash);

			if (user == null)
			{
				return Result<AuthorizedUser>.Fail("Invalid username or password.");
			}

			var token = tokenService.CreateTokenForUser(username);
			var authorizedUser = new AuthorizedUser(user.Username, user.AccessLevel, token);

			return Result<AuthorizedUser>.Ok(authorizedUser);
		}
	}
}