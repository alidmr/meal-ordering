using MealOrdering.Server.Services.Infrastructure;
using MealOrdering.Shared.Dtos;
using MealOrdering.Shared.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MealOrdering.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ServiceResponse<UserLoginResponseDto>> Login(UserLoginRequestDto UserRequest)
        {
            return new ServiceResponse<UserLoginResponseDto>()
            {
                Value = await _userService.Login(UserRequest.Email, UserRequest.Password)
            };
        }


        [HttpGet("Users")]
        public async Task<ServiceResponse<List<UserDto>>> GetUsers()
        {
            var result = await _userService.GetUsers();
            return new ServiceResponse<List<UserDto>>()
            {
                Value = result
            };
        }

        [HttpPost("Create")]
        public async Task<ServiceResponse<UserDto>> CreateUser([FromBody] UserDto user)
        {
            return new ServiceResponse<UserDto>()
            {
                Value = await _userService.CreateUser(user)
            };
        }

        [HttpPost("Update")]
        public async Task<ServiceResponse<UserDto>> UpdateUser([FromBody] UserDto user)
        {
            return new ServiceResponse<UserDto>() { Value = await _userService.UpdateUser(user) };
        }

        [HttpGet("UserById/{id}")]
        public async Task<ServiceResponse<UserDto>> GetUserById(Guid id)
        {
            return new ServiceResponse<UserDto>()
            {
                Value = await _userService.GetUserById(id)
            };
        }

        [HttpPost("Delete")]
        public async Task<ServiceResponse<bool>> DeleteUser([FromBody] Guid id)
        {
            return new ServiceResponse<bool>()
            {
                Value = await _userService.DeleteUserById(id)
            };
        }
    }
}
