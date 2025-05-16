using Common.Unknown;
using System.Security.Claims;

namespace DTOs.Buffalo
{
    public class LoginResult
    {
        public LoginResultType Result { get; private set; }

        public ClaimsIdentity Identity { get; private set; }

        public LoginResult(LoginResultType result)
        {
            Result = result;
        }

        public LoginResult(ClaimsIdentity identity)
            : this(LoginResultType.Success)
        {
            Identity = identity;
        }
    }
}
