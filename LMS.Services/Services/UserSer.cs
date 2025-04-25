using AutoMapper;
using LMS.Repo.IRepositories;
using LMS.Repo.Models;
using LMS.Services.DTOs;
using LMS.Services.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Services.Services
{
    public class UserSer : IUserSer
    {
        private readonly IUserRepo _userRepo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public UserSer(IUserRepo userRep, IConfiguration config, IMapper mapper)
        {
            _userRepo = userRep;
            _config = config;
            _mapper = mapper;
        }

       

        public async Task<UserDto> LoginAsync(UserDto userDto)
        {
            UserDto result= new UserDto();
            var user = await _userRepo.GetByUsernameAsync(userDto.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(userDto.Password, user.PasswordHash))
                throw new Exception("Invalid credentials");

            result = _mapper.Map<UserDto>(user);
            result.Token = GenerateJwtToken(user);
            return result;
        }

        public async Task<string> RegisterAsync(UserDto userDto)
        {
            var existing = await _userRepo.GetByUsernameAsync(userDto.Username);
            if (existing != null)
                throw new Exception("User already exists.");

            var user = _mapper.Map<Users>(userDto);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

            await _userRepo.AddUserAsync(user);
            return GenerateJwtToken(user);
        }
        private string GenerateJwtToken(Users user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> AddCourses(AddUserCourcesRequestDto addUserCourcesRequestDto)
        {
            List<UserCourses> userCourses = new List<UserCourses>();
            userCourses = _mapper.Map<List<UserCourses>>(addUserCourcesRequestDto.Cources);
          return await _userRepo.AddCourses(userCourses);
        }

        public async Task<bool> UpdateScore(UserCourseDto userCourseDto)
        {
            UserCourses userCourses = new UserCourses();
            userCourses= _mapper.Map<UserCourses>(userCourseDto);
            return await _userRepo.UpdateScore(userCourses);
        }
    }
}
