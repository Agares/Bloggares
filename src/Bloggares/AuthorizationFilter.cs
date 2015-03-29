using Microsoft.AspNet.Mvc;

namespace Bloggares
{
	internal class AuthorizationFilter : IFilter, IActionFilter
	{
		public void OnActionExecuting(ActionExecutingContext context)
		{
		}

		public void OnActionExecuted(ActionExecutedContext context)
		{
		}
	}
}