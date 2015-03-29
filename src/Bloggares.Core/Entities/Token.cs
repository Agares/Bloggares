using System;

namespace Bloggares.Core.Entities
{
	public class Token
	{
		private readonly Guid guid;

		private Token(Guid guid)
		{
			this.guid = guid;
		}

		public static Token Random()
		{
			return new Token(Guid.NewGuid());
		}

		public static explicit operator Token(string from)
		{
			return new Token(Guid.Parse(from));
		}

		public static explicit operator Guid(Token from)
		{
			return from.guid;
		}
	}
}