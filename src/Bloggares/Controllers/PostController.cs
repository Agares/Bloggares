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
		private IUserService userService;

		public PostController(IPostService postService, IUserService userService)
		{
			this.postService = postService;
			this.userService = userService;
		}

		[HttpGet]
		public IEnumerable<Post> Index(Guid token)
		{
			var authorizedUser = userService.Authorize(token);
			if (authorizedUser == null)
			{
				return Enumerable.Empty<Post>();// todo something more fancy maybe?
			}
			return postService.All(authorizedUser.AccessLevel);
		}

		[HttpGet("{slug}")]
		public Post Single(string slug, Guid token)
		{
			// todo move token to header and take care of authorization in base controller
			var authorizedUser = userService.Authorize(token);
			return postService.BySlug(slug, authorizedUser.AccessLevel);
		}
	}
}