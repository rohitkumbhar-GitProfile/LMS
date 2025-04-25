using LMS.Repo.Models;
using LMS.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Services.IServices
{
    public interface IUserSer
    {
        Task<string> RegisterAsync(UserDto userDto);
        Task<UserDto> LoginAsync(UserDto userDto);
        Task<bool> AddCourses(AddUserCourcesRequestDto addUserCourcesRequestDto);
        Task<bool> UpdateScore(UserCourseDto userCourseDto);
    }
}
