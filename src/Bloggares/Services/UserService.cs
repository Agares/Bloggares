﻿using System.Data;
using System.Security.Cryptography;
using System.Text;
using Bloggares.Commands;
using Bloggares.Entities;
using Bloggares.Services.DAL;

namespace Bloggares.Services
{
	public class UserService : IUserService
	{
		private UserDAL userDAL;
		private TokenService tokenService;
		private CryptographyService cryptographyService;

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