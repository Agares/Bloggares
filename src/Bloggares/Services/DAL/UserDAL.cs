﻿using System;
using System.Data;
using System.Linq;
using Bloggares.Entities;
using Dapper;

namespace Bloggares.Services.DAL
{
	public class UserDAL
	{
		private IDbConnection connection;

		public UserDAL(IDbConnection connection)
		{
			this.connection = connection;
		}

		public void Create(string username, byte[] passwordHash, long accessLevel)
		{
			connection.Execute(
				@"INSERT INTO users(username, passwordHash, accessLevel) VALUES(@username, @passwordHash, @accessLevel)",
				new { username, passwordHash, accessLevel }
			);
		}

		internal User FindUserByCredentials(string username, byte[] passwordHash)
		{
			return connection
				.Query<User>(
					@"SELECT username, accessLevel FROM users WHERE username=@Username AND passwordHash=@PasswordHash",
					new { Username = username, PasswordHash = passwordHash }
				)
				.SingleOrDefault();
		}
	}
}