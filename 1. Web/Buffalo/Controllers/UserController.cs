using Abstractions.Interfaces;
using Infrastructure.ApiControllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Buffalo.Controllers
{
    [Produces("application/json")]
    [Route("buffalo/user")]
    [Authorize]
    public class UserController : ApiControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
    }
}
