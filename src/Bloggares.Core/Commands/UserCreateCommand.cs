﻿using Bloggares.Core.CQRS;

namespace Bloggares.Core.Commands
{
	public class UserCreateCommand : ICommand
	{
		public string Username { get; }

		public string Password { get; }

		public long AccessLevel { get; }

		public UserCreateCommand(string username, string password, long accessLevel)
		{
			Username = username;
			Password = password;
			AccessLevel = accessLevel;
		}
	}
}