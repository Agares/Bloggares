using System;

namespace Bloggares.Core.Entities
{
	public class AuthorizedUser : User
	{
		public Token Token { get; private set; }

		[Obsolete("Only for ORM", true)]
		public AuthorizedUser() { }

		public AuthorizedUser(string username, long accessLevel, Token token) : base(username, accessLevel)
		{
			Token = token;
		}
	}
}