using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RozetkaBackEnd.Core.Dtos.User;
using RozetkaBackEnd.Core.Services;
using RozetkaBackEnd.Core.Validations.User;

namespace RozetkaBackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginUserAsync([FromBody] LoginUserDto model)
        {
            var validator = new LoginUserValidation();
            var validationResult = validator.Validate(model);
            if (validationResult.IsValid)
            {
                var result = await _userService.LoginUserAsync(model);
                return Ok(result);
            }
            else
            {
                return BadRequest(validationResult.Errors);
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserDto model)
        {
            RegisterUserValidation validator = new RegisterUserValidation();
            var validationResult = validator.Validate(model);
            if (validationResult.IsValid)
            {
                var result = await _userService.RegisterUserAsync(model);
                return Ok(result);
            }
            else
            {
                return BadRequest(validationResult.Errors);
            }
        }
    }
}
