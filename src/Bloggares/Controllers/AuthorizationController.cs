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

		public AuthorizationController(IUserService userService)
		{
			this.userService = userService;
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
			return userService.GetUserByToken(token)
				.Then(user => user, message => { throw new Exception(message); });
		}
	}
}