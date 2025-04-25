using LMS.Repo.Models;
using LMS.Services.DTOs;
using LMS.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserSer _userService;

        public UserController(IUserSer userSer)
        {
            _userService = userSer;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            var token = await _userService.RegisterAsync(userDto);

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new { Message = "Registration failed. Please try again." });
            }

            return Ok(new
            {
                Token = token,
                Message = "Registration successful"
            });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserDto userDto)
        {
            var user = await _userService.LoginAsync(userDto);

            if (user ==null)
            {
                return Unauthorized(new { Message = "Invalid credentials" });
            }

            return Ok(new
            {
                Result = user,
                Message = "Login successful"
            });
        }

        [HttpPost("AssignCourses")]
        public async Task<IActionResult> AssignCources([FromBody] AddUserCourcesRequestDto userDto)
        {
            string message = "";
            bool isCourceAssigned=false;
            try
            {
                if (userDto.Cources.Count>0)
                {
                    isCourceAssigned= await _userService.AddCourses(userDto);
                }
            }
            catch (Exception ex) {
                message=ex.Message;
            }
            return Ok(new
            {
                Result = isCourceAssigned,
                Message = "Cources Added successfully"
            });
        }

        [HttpPost("UpdateScore")]
        public async Task<IActionResult> UpdateScore([FromBody] UserCourseDto userDto)
        {
            string message = "";
            bool isScoreUpdated = false;
            try
            {
                if (userDto !=null)
                {
                    isScoreUpdated = await _userService.UpdateScore(userDto);
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Ok(new
            {
                Result = isScoreUpdated,
                Message = "Score Updated Successfully"
            });
        }


        [HttpPost("CheckAttention/{userId}")]
        public async Task<IActionResult> CheckAttention(int userId)
        {
            //var user = await _userService.LoginAsync(userDto);

            // if (user ==null)
            // {
            //     return Unauthorized(new { Message = "Invalid credentials" });
            // }

            // return Ok(new
            // {
            //     Result = null,
            //     Message = "Login successful"
            // });
        }
    }
}
