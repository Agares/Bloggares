using System;

namespace Bloggares.Common.Entities
{
	public class Post
	{
		[Obsolete("Only for ORM", true)]
		public Post() { }

		public Post(string slug, string title, string content)
		{
			Slug = slug;
			Title = title;
			Content = content;
		}

		public string Slug { get; private set; }

		public string Title { get; private set; }

		public string Content { get; private set; }
	}
}