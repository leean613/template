using Abstractions.Interfaces;
using Common.Helpers;
using Common.Runtime.Security;
using DTOs.Buffalo;
using Microsoft.Extensions.Options;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class JwtTokenService : ITokenService
    {
        private readonly JwtIssuerOptions _jwtOptions;

        public JwtTokenService(IOptions<JwtIssuerOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_jwtOptions);
        }

        public async Task<JwtTokenResultDto> RequestTokenAsync(LoginResult loginResult)
        {
            return new JwtTokenResultDto
            {
                Id = loginResult.Identity.GetId(),
                UserImageURL = loginResult.Identity.GetUserImageURL(),
                TokenType = "Bearer",
                AccessToken = await GenerateEncodedToken(loginResult.Identity),
            };
        }

        private async Task<string> GenerateEncodedToken(ClaimsIdentity identity)
        {
            _jwtOptions.IssuedAt = DateTime.Now;

            var claims = identity.Claims.Union(
                new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, identity.Name),
                    new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                    new Claim(
                        JwtRegisteredClaimNames.Iat,
                        _jwtOptions.IssuedAt.LocalToUtcTime().ToSecondsTimestamp().ToString(),
                        ClaimValueTypes.Integer64),
                    new Claim(ClaimTypes.System, identity.GetUserSystem()),
                });

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                _jwtOptions.Issuer,
                _jwtOptions.Audience,
                claims,
                _jwtOptions.IssuedAt,
                _jwtOptions.IssuedAt.AddHours(_jwtOptions.ExpirationInHours),
                _jwtOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        private void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));

            if (options.SigningCredentials == null)
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));

            if (options.JtiGenerator == null)
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
        }
    }
}
