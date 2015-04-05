using System;

namespace Bloggares.Core.Services.DAL
{
	public interface ITokenDAL
	{
		void CreateTokenForUser(string username, Guid token, DateTime validUntil);
	}
}