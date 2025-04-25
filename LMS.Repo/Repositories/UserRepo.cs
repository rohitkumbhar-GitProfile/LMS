using LMS.Repo.IRepositories;
using LMS.Repo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LMS.Repo.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly AppDbContext _context;

        public UserRepo(AppDbContext context)
        {
            _context = context;
        }
       

       public async Task<Users> GetByUsernameAsync(string username)
        {
           return await _context.Users.FirstOrDefaultAsync(u => u.Username == username).ConfigureAwait(false);
        }

        public async Task AddUserAsync(Users user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AddCourses(List<UserCourses> addUserCources)
        {
            _context.UserCourses.AddRange(addUserCources);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateScore(UserCourses UpdateScore)
        {
           UserCourses uc= await _context.UserCourses.Where(uc => uc.UserId == UpdateScore.UserId && uc.Title == UpdateScore.Title).FirstOrDefaultAsync();
            if (uc != null)
            {
                uc.QuizScore =UpdateScore.QuizScore;
                _context.UserCourses.Update(uc);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
