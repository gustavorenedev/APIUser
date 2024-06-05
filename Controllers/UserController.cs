using Microsoft.AspNetCore.Mvc;
using ProjectFor7COMm.Services;

namespace ProjectFor7COMm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

    }
}
