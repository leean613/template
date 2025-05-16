using System;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Principal;

namespace Common.Runtime.Security
{
    public static class ClaimsIdentityExtensions
    {
        public static string GetUserId(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException();
            }

            var userIdClaim = (identity as ClaimsIdentity)?.Claims.FirstOrDefault(c => c.Type == GlotechClaimTypes.UserId);
            if (string.IsNullOrEmpty(userIdClaim?.Value))
            {
                throw new AuthenticationException();
            }

            return userIdClaim.Value;
        }

        public static Guid GetId(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException();
            }

            var userIdClaim = (identity as ClaimsIdentity)?.Claims.FirstOrDefault(c => c.Type == GlotechClaimTypes.Id);
            if (string.IsNullOrEmpty(userIdClaim?.Value))
            {
                throw new AuthenticationException();
            }

            Guid id;
            if (!Guid.TryParse(userIdClaim.Value, out id) && !string.IsNullOrWhiteSpace(userIdClaim.Value))
            {
                throw new AuthenticationException();
            }

            return id;
        }

        public static string GetUserSystem(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException();
            }

            var userRoleClaim = (identity as ClaimsIdentity)?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.System);
            if (string.IsNullOrEmpty(userRoleClaim?.Value))
            {
                throw new AuthenticationException();
            }

            return userRoleClaim.Value;
        }

        public static string GetUserName(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException();
            }

            var userNameClaim = (identity as ClaimsIdentity)?.Claims.FirstOrDefault(c => c.Type == GlotechClaimTypes.UserName);
            if (string.IsNullOrEmpty(userNameClaim?.Value))
            {
                throw new AuthenticationException();
            }

            return userNameClaim.Value;
        }

        public static string GetUserImageURL(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException();
            }

            var userImageClaim = (identity as ClaimsIdentity)?.Claims.FirstOrDefault(c => c.Type == GlotechClaimTypes.UserImageURL);

            return userImageClaim.Value;
        }

        public static bool GetIsAdmin(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException();
            }

            var userAdminClaim = (identity as ClaimsIdentity)?.Claims.FirstOrDefault(c => c.Type == GlotechClaimTypes.IsAdmin);
            if (string.IsNullOrEmpty(userAdminClaim?.Value))
            {
                throw new AuthenticationException();
            }

            bool isAdmin;
            if (!bool.TryParse(userAdminClaim.Value, out isAdmin) && !string.IsNullOrWhiteSpace(userAdminClaim.Value))
            {
                throw new AuthenticationException();
            }

            return isAdmin;
        }
    }
}
