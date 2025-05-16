using Abstractions.Interfaces;
using Common.Unknown;
using DTOs.Buffalo;
using Infrastructure.ApiControllers;
using Infrastructure.ApiResults;
using Infrastructure.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Common.Resources;

namespace Buffalo.Controllers
{
    [Produces("application/json")]
    [Route("buffalo")]
    [Authorize]
    public class LoginController : ApiControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly ITokenService _tokenService;

        public LoginController(ITokenService tokenService, ILoginService loginService)
        {
            _tokenService = tokenService;
            _loginService = loginService;
        }

        /// <summary>
        /// Login
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        [ModelValidationFilter]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var loginResult = await _loginService.LoginAsync(dto);

            switch (loginResult.Result)
            {
                case LoginResultType.InvalidUserNameOrPassword:
                    return BadRequest(ApiErrorCodes.UserLoginInvalidUserNameOrPassword, ExceptionResource.InvalidUsernameOrPassword);

                case LoginResultType.UserIsNotActive:
                    return BadRequest(ApiErrorCodes.UserIsNotActive, ExceptionResource.UserIsNotActive);

                case LoginResultType.Success:
                    var jwtToken = await _tokenService.RequestTokenAsync(loginResult);
                    return Success(jwtToken);

                default:
                    return BadRequest(ApiErrorCodes.InvalidSystem, ExceptionResource.InvalidSystem);
            }
        }
    }
}
