using System;
using System.Net;
using Bloggares.Core.Entities;
using Bloggares.Core.Services;
using Microsoft.AspNet.Mvc;

namespace Bloggares.Controllers
{
	public class AuthorizeFilter : IActionFilter
	{
		private readonly ITokenService tokenService;

		public AuthorizeFilter(ITokenService tokenService)
		{
			this.tokenService = tokenService;
		}

		public void OnActionExecuting(ActionExecutingContext context)
		{
			string token = context.HttpContext.Request.Headers["X-Bloggares-Token"];
			if (string.IsNullOrEmpty(token))
			{
				context.Result = new HttpStatusCodeResult((int)HttpStatusCode.Forbidden);
				return;
			}

			tokenService
				.GetUserByToken(new Guid(token))
				.Then
				(
					authorizedUser => { InjectUserToController(context, authorizedUser); },
					errorMessage => context.Result = new HttpStatusCodeResult((int)HttpStatusCode.Forbidden)
				);
		}

		private static void InjectUserToController(ActionExecutingContext context, AuthorizedUser x)
		{
			var userAwareController = context.Controller as IUserAwareController;
			if (userAwareController != null)
			{
				userAwareController.CurrentUser = x;
			}
		}

		public void OnActionExecuted(ActionExecutedContext context)
		{
		}
	}
}