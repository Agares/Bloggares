using System;
using Bloggares.Entities;
using Bloggares.Services;
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
			return userService.Authorize(username, password);
		}

		[HttpPost("check-token")]// todo rename to validate-token?
		public AuthorizedUser Authorize(Guid token)	// todo rename to ValidateToken
		{
			return userService.Authorize(token);
		}
	}
}