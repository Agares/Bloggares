using System;
using Bloggares.Core.Services.DAL;

namespace Bloggares.Core.Services
{
	public class TokenService : ITokenService
	{
		private readonly ITokenDAL tokenDAL;

		public TokenService(ITokenDAL tokenDAL)
		{
			this.tokenDAL = tokenDAL;
		}

		public Guid CreateTokenForUser(string username)
		{
			var token = Guid.NewGuid();
			tokenDAL.CreateTokenForUser(username, token, DateTime.Now.AddDays(1)); // todo configurable validity time
			return token;
		}
	}
}