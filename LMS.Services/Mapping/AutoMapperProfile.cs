using AutoMapper;
using LMS.Repo.Models;
using LMS.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Services.Mapping
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserDto,Users>().ReverseMap();
            CreateMap<UserQuizScoreDto, UserQuizScore>();
            CreateMap<UserCourseDto, UserCourses>().ReverseMap();
            CreateMap<AddUserCourcesRequestDto, UserCourses>().ReverseMap();

        }
    }
}
