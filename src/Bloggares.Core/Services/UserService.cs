using System;
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

		public Result<AuthorizedUser> Authorize(string username, string password)
		{
			var passwordHash = cryptographyService.HashPassword(password);
			var user = userDAL.FindUserByCredentials(username, passwordHash);

			if (user == null)
			{
				return Result.Fail<AuthorizedUser>("Invalid username or password.");
			}

			var token = tokenService.CreateTokenForUser(username);
			var authorizedUser = new AuthorizedUser(user.Username, user.AccessLevel, token);

			return Result.Ok(authorizedUser);
		}

		public Result<AuthorizedUser> GetUserByToken(Guid token)
		{
			var user = userDAL.FindUserByToken(token);

			return user == null
				? Result.Fail<AuthorizedUser>("Invalid or expired token.")
				: Result.Ok(user);
		}
	}
}