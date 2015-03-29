using Microsoft.Framework.ConfigurationModel;

namespace Bloggares.Core
{
	public interface IConfigurationProvider
	{
		Configuration Configuration { get; }
	}
}