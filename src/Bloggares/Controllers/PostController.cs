using System.Collections.Generic;
using Bloggares.Core.Entities;
using Bloggares.Core.Services;
using Microsoft.AspNet.Mvc;

namespace Bloggares.Controllers
{
	[Route("api/posts")]
	public class PostController : Controller, IUserAwareController
	{
		private IPostService postService;
		private ITokenService tokenService;
		private IUserService userService;

		public PostController(IPostService postService, IUserService userService, ITokenService tokenService)
		{
			this.postService = postService;
			this.userService = userService;
			this.tokenService = tokenService;
		}

		public AuthorizedUser CurrentUser { set; get; }

		[HttpGet, Authorize]
		public IEnumerable<Post> Index()
		{
			return postService.All(CurrentUser.AccessLevel);
		}

		[HttpGet("{slug}"), Authorize]
		public Post Single(string slug)
		{
			return postService.BySlug(slug, CurrentUser.AccessLevel);
		}
	}
}