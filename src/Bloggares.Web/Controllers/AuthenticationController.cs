using Bloggares.Common;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;

namespace Bloggares.Web.Controllers
{
	[Route("login")]
	public class AuthenticationController : Controller
	{
		private readonly IAuthenticationClient authenticationClient;
		private readonly IUserHolder userHolder;

		public AuthenticationController(IAuthenticationClient authenticationClient, IUserHolder userHolder)
		{
			this.authenticationClient = authenticationClient;
			this.userHolder = userHolder;
		}

		[HttpGet, Route("", Name = "login.index")]
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost, Route("", Name = "login.execute")]
		public IActionResult Execute(string username, string password)
		{
			var user = authenticationClient.Authenticate(username, password);
			userHolder.SetUser(user);

			return View(user);
		}
	}
}