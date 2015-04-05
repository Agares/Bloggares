using Bloggares.Common;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Bloggares.Web.Controllers
{
	[Route("posts")]
	public class PostController : Controller
	{
		private readonly IUserHolder userHolder;
		private readonly IPostClient postClient;

		public PostController(IUserHolder userHolder, IPostClient postClient)
		{
			this.userHolder = userHolder;
			this.postClient = postClient;
		}

		[Route("")]
		public IActionResult Index()
		{
			return userHolder.User.Then<IActionResult>(
				user => View(postClient.All(user)),
				error => RedirectToAction("Index", "Authentication")
				);
		}
	}
}