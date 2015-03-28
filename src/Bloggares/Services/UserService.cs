using System;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Bloggares.Commands;
using Bloggares.Entities;
using Dapper;

namespace Bloggares.Services
{
	// todo keep sql queries out of services
	public class UserService : IUserService
	{
		private IDbConnection connection;

		public UserService(IDbConnection connection)
		{
			this.connection = connection;
		}

		// todo hashing shall be done in other service
		private byte[] HashPassword(string password)
		{
			var hmac = SHA512.Create();
			return hmac.ComputeHash(Encoding.UTF32.GetBytes(password));
		}

		public void Create(UserCreateCommand command)
		{
			var passwordHash = HashPassword(command.Password);

			connection.Execute(
				@"INSERT INTO users(username, passwordHash, accessLevel) VALUES(@Username, @PasswordHash, @AccessLevel)",
				new { Username = command.Username, PasswordHash = passwordHash, AccessLevel = command.AccessLevel }
			);
		}

		// todo custom token type?
		// todo cache?
		// todo change method name?
		// todo move to tokenservice?
		public AuthorizedUser Authorize(Guid token)
		{
			var user = connection
				.Query<AuthorizedUser>(
					@"SELECT u.username, u.accessLevel, t.token FROM Users u
						LEFT JOIN tokens t ON t.username=u.username
						WHERE t.token=@Token AND t.validUntil > NOW()",
					new { Token = token }
				)
				.SingleOrDefault();

			return user;
		}

		public AuthorizedUser Authorize(string username, string password)
		{
			var passwordHash = HashPassword(password);

			var user = connection
				.Query(
					@"SELECT username, accessLevel FROM users WHERE username=@Username AND passwordHash=@PasswordHash",
					new { Username = username, PasswordHash = passwordHash }
				)
				.SingleOrDefault();

			if (user == null)
			{
				return null; // todo throw new AuthorizationException?
			}

			// todo token service
			// todo garbage collection
			var token = Guid.NewGuid();
			connection.Execute(
				@"INSERT INTO tokens(username, token, validUntil) VALUES(@Username, @Token, @ValidUntil)",
				new { Username = username, Token = token, ValidUntil = DateTime.Now.AddDays(1) } // todo configurable validity time
			);
			var authorizedUser = new AuthorizedUser(user.Username, user.AccessLevel, token);

			return authorizedUser;
		}
	}
}