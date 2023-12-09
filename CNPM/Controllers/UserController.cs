using CNPM.Domain;
using CNPM.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CNPM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserSessionState _userSessionState;

        private readonly UserService _userService;

        public UserController(UserService userService, UserSessionState userSessionState)
        {
            _userSessionState = userSessionState;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserCUDto userCUDto)
        {
            await _userService.Create(userCUDto);

            return StatusCode(StatusCodes.Status200OK);
        }

        [Authorize]
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] string newPassword)
        {
            if (_userSessionState.User_Id == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }

            await _userService.ChangePassword((int)_userSessionState.User_Id, newPassword);

            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
