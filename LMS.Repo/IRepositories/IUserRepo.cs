using LMS.Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Repo.IRepositories
{
    public interface IUserRepo
    {
        Task<Users> GetByUsernameAsync(string username);
        Task AddUserAsync(Users user);
        Task<bool> AddCourses(List<UserCourses> addUserCources);
        Task<bool> UpdateScore(UserCourses UpdateScore);

    }
}
