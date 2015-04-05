using System;
using Bloggares.Common.Entities;
using RestSharp;

namespace Bloggares.Common
{
	public class AuthenticationClient : IAuthenticationClient
	{
		private readonly IRestClient restClient;

		public AuthenticationClient(IRestClient restClient)
		{
			this.restClient = restClient;
		}

		public AuthorizedUser Authenticate(string username, string password)
		{
			var request = new RestRequest("authorization", Method.POST);
			request.AddParameter("username", username);
			request.AddParameter("password", password);

			IRestResponse<AuthorizedUser> response = restClient.Execute<AuthorizedUser>(request);
			return response.Data;
		}

		public AuthorizedUser GetUserByToken(Guid token)
		{
			var request = new RestRequest("authorization/get-user-by-token", Method.POST);
			request.AddParameter("token", token);

			IRestResponse<AuthorizedUser> response = restClient.Execute<AuthorizedUser>(request);
			return response.Data;
		}
	}
}