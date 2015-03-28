using System;

namespace Bloggares.Entities
{
	public class User
	{
		public string Username { get; private set; }

		public long AccessLevel { get; private set; }

		[Obsolete("Only for ORM", true)]
		public User() { }

		public User(string username, long accessLevel)
		{
			Username = username;
			AccessLevel = accessLevel;
		}
	}
}