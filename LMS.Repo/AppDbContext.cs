using LMS.Repo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Repo
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Users> Users { get; set; }
        public DbSet<UserCourses> UserCourses { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }
        //public DbSet<UserQuizScore> UsersQuiz { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().HasKey(u => u.Id);
            modelBuilder.Entity<UserCourses>().HasKey(uc => uc.UserCourseId);
            //modelBuilder.Entity<UserQuizScore>().HasKey(uq => uq.UserQuizId);
        }
    }
}
