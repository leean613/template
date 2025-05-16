using Common.Runtime.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Infrastructure.ContainerConfigs
{
    public static class AuthServicesInstallerAWS
    {
        private const string SecretKey = "GlotechKeyApiDontShared123!@#"; // todo: get this from somewhere secure

        private static readonly SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        public static void ConfigureServicesAuth(IServiceCollection services, IConfiguration configuration)
        {
            var jwtAppSettingOptions = configuration.GetSection(nameof(AWSInfoOptions));

            // Configure JwtIssuerOptions
            services.Configure<AWSInfoOptions>(
                options =>
                {
                    options.AccessKey = jwtAppSettingOptions[nameof(AWSInfoOptions.AccessKey)];
                    options.SecretKey = jwtAppSettingOptions[nameof(AWSInfoOptions.SecretKey)];
                    options.Region = jwtAppSettingOptions[nameof(AWSInfoOptions.Region)];
                    options.UserPoolID = jwtAppSettingOptions[nameof(AWSInfoOptions.UserPoolID)];
                    options.ClientAppID = jwtAppSettingOptions[nameof(AWSInfoOptions.ClientAppID)];
                    options.S3BucketName = jwtAppSettingOptions[nameof(AWSInfoOptions.S3BucketName)];
                    options.S3UserImagePath = jwtAppSettingOptions[nameof(AWSInfoOptions.S3UserImagePath)];
                    options.VPNPublicIP1 = jwtAppSettingOptions[nameof(AWSInfoOptions.VPNPublicIP1)];
                    options.VPNPublicIP2 = jwtAppSettingOptions[nameof(AWSInfoOptions.VPNPublicIP2)];
                    options.TokenExpirationInHours = Convert.ToInt32(jwtAppSettingOptions[nameof(AWSInfoOptions.TokenExpirationInHours)]);
                });

            services
                .AddAuthentication(
                    options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                .AddJwtBearer(
                    configureOptions =>
                    {
                        configureOptions.SaveToken = true;
                        configureOptions.TokenValidationParameters = GetCognitoTokenValidationParams(jwtAppSettingOptions);
                    });
        }

        private static TokenValidationParameters GetCognitoTokenValidationParams(IConfigurationSection configurationSection)
        {
            var region = configurationSection[nameof(AWSInfoOptions.Region)];
            var userPoolId = configurationSection[nameof(AWSInfoOptions.UserPoolID)];
            var cognitoIssuer = $"https://cognito-idp.{region}.amazonaws.com/{userPoolId}";
            var jwtKeySetUrl = $"{cognitoIssuer}/.well-known/jwks.json";

            return new TokenValidationParameters
            {
                IssuerSigningKeyResolver = (s, securityToken, identifier, parameters) =>
                {
                    // get JsonWebKeySet from AWS
                    var json = new WebClient().DownloadString(jwtKeySetUrl);

                    // serialize the result
                    var keys = JsonConvert.DeserializeObject<JsonWebKeySet>(json).Keys;

                    // cast the result to be the type expected by IssuerSigningKeyResolver
                    return (IEnumerable<SecurityKey>)keys;
                },
                ValidIssuer = cognitoIssuer,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateAudience = false,
            };
        }
    }
}
