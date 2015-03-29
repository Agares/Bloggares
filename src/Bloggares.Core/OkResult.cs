using System;

namespace Bloggares.Core
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
	}
}