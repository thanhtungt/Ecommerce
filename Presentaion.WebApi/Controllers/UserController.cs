using AutoMapper;
using Business.Domain.System.User;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(LoginModel model)
        {
            var loginEntity = _mapper.Map<LoginModel, LoginRequestEntity>(model);
            var result = await _userService.AuthenticateAsync(loginEntity);
            if (result.IsSuccessed) { return Ok(result.Result); }
            return Unauthorized(result.Message);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var registerEntity = _mapper.Map<RegisterModel, RegisterRequestEntity>(model);
            var result = await _userService.RegisterAsync(registerEntity);
            if (result.IsSuccessed) { return Ok(); }
            return BadRequest(result.Message);
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await _userService.GetAllRolesAsync();
            if (result.IsSuccessed)
            {
                return Ok(result.Result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUser()
        {
            var result = await _userService.GetAllUserInformationAsync();
            if (result.IsSuccessed)
            {
                return Ok(result.Result);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteUser([FromForm] Guid id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (result.IsSuccessed)
            {
                return StatusCode(StatusCodes.Status202Accepted);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("setAdmin")]
        public async Task<IActionResult> SetAdmin([FromForm] Guid id, [FromForm] bool isAdmin)
        {
            var result = await _userService.SetAdmin(id, isAdmin);
            if (result.IsSuccessed)
            {
                return StatusCode(StatusCodes.Status202Accepted);
            }
            return BadRequest(result.Message);
        }
    }
}
