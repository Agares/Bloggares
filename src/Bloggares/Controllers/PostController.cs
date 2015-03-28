using System;
using System.Collections.Generic;
using System.Linq;
using Bloggares.Database;
using Bloggares.Entities;
using Bloggares.Services;
using Microsoft.AspNet.Mvc;

namespace Bloggares.Controllers
{
	[Route("api/posts")]
	public class PostController : Controller
	{
		private IPostService postService;
		private TokenService tokenService;
		private IUserService userService;

		public PostController(IPostService postService, IUserService userService, TokenService tokenService)
		{
			this.postService = postService;
			this.userService = userService;
			this.tokenService = tokenService;
		}

		[HttpGet]
		public IEnumerable<Post> Index(Guid token)
		{
			var authorizedUser = tokenService.GetUserByToken(token);

			return authorizedUser
				.Then(user => postService.All(user.AccessLevel), message => { throw new Exception(message); });
		}

		[HttpGet("{slug}")]
		public Post Single(string slug, Guid token)
		{
			// todo move token to header and take care of authorization in base controller
			var authorizedUser = tokenService.GetUserByToken(token);
			return authorizedUser
				.Then(user => postService.BySlug(slug, user.AccessLevel), message => { throw new Exception(message); });
		}
	}
}