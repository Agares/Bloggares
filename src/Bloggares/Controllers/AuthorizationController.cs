using System;
using Bloggares.Common.Entities;
using Bloggares.Core.Services;
using Microsoft.AspNet.Mvc;

namespace Bloggares.WebService.Controllers
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

		// todo why is that POST? should be GET
		[HttpPost("get-user-by-token")]
		public AuthorizedUser GetUserByToken(Guid token)
		{
			return userService.GetUserByToken(token)
				.Then(user => user, message => { throw new Exception(message); });
		}
	}
}