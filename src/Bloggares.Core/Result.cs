using System;

namespace Bloggares.Core
{
	public abstract class Result<T>
	{
		public static Result<T> Ok(T resultValue)
		{
			return new OkResult<T>(resultValue);
		}

		public static Result<T> Fail(string message)
		{
			return new FailResult<T>(message);
		}

		public abstract TOther Then<TOther>(Func<T, TOther> successAction, Func<string, TOther> errorAction);
	}
}