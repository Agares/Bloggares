using System;

namespace Bloggares.Core
{
	public class FailResult<T> : Result<T>
	{
		private string Message { get; }

		public FailResult(string message)
		{
			Message = message;
		}

		public override TOther Then<TOther>(Func<T, TOther> successAction, Func<string, TOther> errorAction)
		{
			return errorAction(Message);
		}

		public override void Then(Action<T> successAction, Action<string> errorAction)
		{
			errorAction(Message);
		}
	}
}