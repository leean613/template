using Common.Constants;
using Microsoft.Extensions.Configuration;
using System;

namespace Common.Helpers
{
    public static class ConfigurationHelper
    {
        public static string GetConnectionStringByEnv(this IConfigurationRoot config)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            return config.GetConnectionString(env != null ? env : EnvironmentTypes.Development);
        }
    }
}
