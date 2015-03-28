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
	}

	public class OkResult<T> : Result<T>
	{
		public T Value { get; }

		public OkResult(T resultValue)
		{
			Value = resultValue;
		}

		public override TOther Then<TOther>(Func<T, TOther> successAction, Func<string, TOther> errorAction)
		{
			return successAction(Value);
		}
	}
}