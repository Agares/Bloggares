using System;
using System.Data;
using Dapper;

namespace Bloggares.Core.Services.DAL
{
	public class TokenDAL : ITokenDAL
	{
		private readonly IDbConnection connection;

		public TokenDAL(IDbConnection connection)
		{
			this.connection = connection;
		}

		public void CreateTokenForUser(string username, Guid token, DateTime validUntil)
		{
			connection.Execute(
				@"INSERT INTO tokens(username, token, validUntil) VALUES(@username, @token, @validUntil)",
				new { username, token = token, validUntil }
			);
		}
	}
}