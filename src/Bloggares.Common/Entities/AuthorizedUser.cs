using System;

namespace Bloggares.Common.Entities
{
	public class AuthorizedUser : User
	{
		public Guid Token { get; private set; }

		[Obsolete("Only for ORM", true)]
		public AuthorizedUser() { }

		public AuthorizedUser(string username, long accessLevel, Guid token) : base(username, accessLevel)
		{
			Token = token;
		}
	}
}