using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Bloggares.Controllers
{
	public class AuthorizeAttribute : TypeFilterAttribute
	{
		public long MinimalAccessLevel { get; }

		public AuthorizeAttribute(long minimalAccessLevel = long.MaxValue)
			: base(typeof(AuthorizeFilter))
		{
			MinimalAccessLevel = minimalAccessLevel;
		}
	}
}