using System;
using Bloggares.Core.Entities;

namespace Bloggares.Core.Services.DAL
{
	public interface ITokenDAL
	{
		void CreateTokenForUser(string username, Token token, DateTime validUntil);
	}
}