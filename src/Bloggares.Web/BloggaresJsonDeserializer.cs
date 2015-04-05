using RestSharp;
using RestSharp.Deserializers;
using ServiceStack.Text;

namespace Bloggares.Web.Controllers
{
	internal class BloggaresJsonDeserializer : IDeserializer
	{
		public string RootElement { get; set; }

		public string Namespace { get; set; }

		public string DateFormat { get; set; }

		public T Deserialize<T>(IRestResponse response)
		{
			return JsonSerializer.DeserializeFromString<T>(response.Content);
		}
	}
}