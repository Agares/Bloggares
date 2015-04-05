using System;
using System.Linq;
using Bloggares.Common;
using Bloggares.Common.Entities;
using Bloggares.Common.Result;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;

namespace Bloggares.Web
{
	public class UserHolder : IUserHolder
	{
		private const string UserTokenSessionKey = "user-token";

		private readonly ISessionCollection session;
		private readonly IAuthenticationClient authenticationClient;

		private AuthorizedUser user;

		public Result<AuthorizedUser> User => user == null
			? RetriveUserFromSession()
			: Result.Ok(user);

		public UserHolder(IHttpContextAccessor httpContext, IAuthenticationClient authenticationClient)
		{
			this.authenticationClient = authenticationClient;
			session = httpContext.Value.Session;
		}

		public void SetUser(AuthorizedUser authorizedUser)
		{
			user = authorizedUser;
			session.SetString(UserTokenSessionKey, authorizedUser.Token.ToString());
		}

		private Result<AuthorizedUser> RetriveUserFromSession()
		{
			if (session.All(x => x.Key != UserTokenSessionKey))
			{
				return Result.Fail<AuthorizedUser>("No token in session.");
			}

			string token = session.GetString(UserTokenSessionKey);
			AuthorizedUser authorizedUser = authenticationClient.GetUserByToken(new Guid(token));

			return authorizedUser == null
				? Result.Fail<AuthorizedUser>("Invalid or expired token")
				: Result.Ok(authorizedUser);
		}
	}
}