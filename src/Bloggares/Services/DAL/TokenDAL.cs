using System;
using System.Data;
using System.Linq;
using Bloggares.Entities;
using Dapper;

namespace Bloggares.Services.DAL
{
	public class TokenDAL
	{
		private IDbConnection connection;

		public TokenDAL(IDbConnection connection)
		{
			this.connection = connection;
		}

		public AuthorizedUser GetUserByToken(Guid token)
		{
			return connection
				.Query<AuthorizedUser>(
					@"SELECT u.username, u.accessLevel, t.token FROM Users u
						LEFT JOIN tokens t ON t.username=u.username
						WHERE t.token=@token AND t.validUntil > NOW()",
					new { token }
				)
				.SingleOrDefault();
		}

		public void CreateTokenForUser(string username, Guid token, DateTime validUntil)
		{
			connection.Execute(
				@"INSERT INTO tokens(username, token, validUntil) VALUES(@username, @token, @validUntil)",
				new { username, token, validUntil }
			);
		}
	}
}