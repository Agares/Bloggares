﻿using Bloggares.Core.Entities;

namespace Bloggares.Core.Services.DAL
{
	public interface IUserDAL
	{
		void Create(string username, byte[] passwordHash, long accessLevel);
		User FindUserByCredentials(string username, byte[] passwordHash);
	}
}