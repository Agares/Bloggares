using System;

namespace Bloggares.Common.Result
{
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

		public override void Then(Action<T> successAction, Action<string> errorAction)
		{
			successAction(Value);
		}

		public override void Then(Action successAction, Action errorAction)
		{
			successAction();
		}

		public override TOther Then<TOther>(Func<TOther> successAction, Func<TOther> errorAction)
		{
			return successAction();
		}
	}
}