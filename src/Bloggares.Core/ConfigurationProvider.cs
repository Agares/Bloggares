using System;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.ConfigurationModel.Json;

namespace Bloggares.Core
{
	public class ConfigurationProvider
	{
		private readonly Lazy<Configuration> configuration;

		public Configuration Configuration => configuration.Value;

		public ConfigurationProvider()
		{
			configuration = new Lazy<Configuration>(CreateConfiguration);
		}

		private Configuration CreateConfiguration()
		{
			var config = new Configuration();
			config.AddJsonFile("config.json");

			return config;
		}
	}
}