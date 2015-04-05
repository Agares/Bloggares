using System;

namespace Bloggares.Common.Result
{
	public abstract class Result
	{
		public static Result<T> Ok<T>(T resultValue)
		{
			return new OkResult<T>(resultValue);
		}

		public static Result<T> Fail<T>(string message)
		{
			return new FailResult<T>(message);
		}

		public abstract void Then(Action successAction, Action errorAction);

		public abstract TOther Then<TOther>(Func<TOther> successAction, Func<TOther> errorAction);
	}

	public abstract class Result<T> : Result
	{
		public abstract TOther Then<TOther>(Func<T, TOther> successAction, Func<string, TOther> errorAction);

		public abstract void Then(Action<T> successAction, Action<string> errorAction);
	}
}