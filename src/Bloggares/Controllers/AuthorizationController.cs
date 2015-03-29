using System;
using Bloggares.Core.Entities;
using Bloggares.Core.Services;
using Microsoft.AspNet.Mvc;

namespace Bloggares.Controllers
{
	[Route("api/authorization")]
	public class AuthorizationController : Controller
	{
		private IUserService userService;

		private ITokenService tokenService;

		public AuthorizationController(IUserService userService, ITokenService tokenService)
		{
			this.userService = userService;
			this.tokenService = tokenService;
		}

		[HttpPost]
		public AuthorizedUser Authorize(string username, string password)
		{
			return userService.Authorize(username, password)
				.Then(user => user, message => { throw new Exception(message); });
		}

		[HttpPost("get-user-by-token")]
		public AuthorizedUser GetUserByToken(Guid token)
		{
			return tokenService.GetUserByToken(token)
				.Then(user => user, message => { throw new Exception(message); });
		}
	}
}