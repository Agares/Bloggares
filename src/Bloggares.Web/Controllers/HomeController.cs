using Microsoft.AspNet.Mvc;

namespace Bloggares.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly IUserHolder userHolder;

		public HomeController(IUserHolder userHolder)
		{
			this.userHolder = userHolder;
		}

		[HttpGet, Route("")]
		public IActionResult Index()
		{
			return userHolder.User.Then<IActionResult>(user => View(user), error => RedirectToAction("Index", "Authentication"));
		}

		public IActionResult Error()
		{
			return View("~/Views/Shared/Error.cshtml");
		}
	}
}