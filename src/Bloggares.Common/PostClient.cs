using System;
using System.Collections;
using System.Collections.Generic;
using Bloggares.Common.Entities;
using RestSharp;

namespace Bloggares.Common
{
	public class PostClient : IPostClient
	{
		private readonly IRestClient restClient;

		public PostClient(IRestClient restClient)
		{
			this.restClient = restClient;
		}

		public IEnumerable<Post> All(AuthorizedUser user)
		{
			var request = new RestRequest("posts", Method.GET);
			request.AddHeader("X-Bloggares-Token", user.Token.ToString());

			return restClient.Execute<List<Post>>(request).Data;
		}

		public Post Single(string slug, AuthorizedUser user)
		{
			var request = new RestRequest("posts/{id}", Method.GET);
			request.AddParameter("id", slug, ParameterType.UrlSegment);
			request.AddHeader("X-Bloggares-Token", user.Token.ToString());

			return restClient.Execute<Post>(request).Data;
		}
	}
}