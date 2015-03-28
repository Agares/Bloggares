using System;
using Bloggares.Core.Entities;
using Bloggares.Core.Services.DAL;

namespace Bloggares.Core.Services
{
	public class TokenService
	{
		private TokenDAL tokenDAL;

		public TokenService(TokenDAL tokenDAL)
		{
			this.tokenDAL = tokenDAL;
		}

		// todo token entity
		// todo cache?
		public Result<AuthorizedUser> GetUserByToken(Guid token)
		{
			var user = tokenDAL.GetUserByToken(token);

			return user == null
				? Result<AuthorizedUser>.Fail("Invalid or expired token.")
				: Result<AuthorizedUser>.Ok(user);
		}

		// todo should be a command
		public Guid CreateTokenForUser(string username)
		{
			var token = Guid.NewGuid();
			tokenDAL.CreateTokenForUser(username, token, DateTime.Now.AddDays(1)); // todo configurable validity time
			return token;
		}
	}
}