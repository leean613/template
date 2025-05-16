using Common.Runtime.Security;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;

namespace Common.Runtime.Session
{
    public class UserSession : IUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimsPrincipal Principal => _httpContextAccessor.HttpContext?.User;

        public UserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int UserId
        {
            get
            {
                var userIdClaim = Principal?.Claims.FirstOrDefault(c => c.Type == GlotechClaimTypes.UserId);
                if (string.IsNullOrEmpty(userIdClaim?.Value))
                {
                    throw new AuthenticationException("User not login to system");
                }

                int userId;
                if (!int.TryParse(userIdClaim.Value, out userId))
                {
                    throw new AuthenticationException("User not login to system");
                }

                return userId;
            }
        }

        public int? GetUserId()
        {
            var userIdClaim = Principal?.Claims.FirstOrDefault(c => c.Type == GlotechClaimTypes.UserId);
            if (string.IsNullOrEmpty(userIdClaim?.Value))
            {
                return null;
            }

            int userId;
            if (!int.TryParse(userIdClaim.Value, out userId))
            {
                return null;
            }

            return userId;
        }

        public string UserName
        {
            get { return Principal?.Claims.FirstOrDefault(c => c.Type == GlotechClaimTypes.CognitoUserName)?.Value; }
        }
    }
}
