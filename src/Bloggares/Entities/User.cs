using System;

namespace Bloggares.Entities
{
	public class AuthorizedUser
	{
		public string Username { get; private set; }

		public long AccessLevel { get; private set; }

		public Guid Token { get; private set; }

		[Obsolete("Only for ORM", true)]
		public AuthorizedUser() { }

		public AuthorizedUser(string username, long accessLevel, Guid token)
		{
			Username = username;
			AccessLevel = accessLevel;
			Token = token;
		}
	}
}