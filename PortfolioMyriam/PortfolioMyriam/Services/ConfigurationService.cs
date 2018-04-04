using Microsoft.Extensions.Configuration;
using System;

namespace PortfolioMyriam.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private IConfiguration _configuration;

        public ConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConfigurationItem(string itemPath)
        {
            return Environment.ExpandEnvironmentVariables(_configuration[itemPath]);
        }
    }
}
