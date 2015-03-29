using Bloggares.Core.Commands;
using Bloggares.Core.Entities;
using Bloggares.Core.Services.DAL;

namespace Bloggares.Core.Services
{
	public class UserService : IUserService
	{
		private readonly IUserDAL userDAL;
		private readonly ITokenService tokenService;
		private readonly ICryptographyService cryptographyService;

		public UserService(IUserDAL userDAL, ITokenService tokenService, ICryptographyService cryptographyService)
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