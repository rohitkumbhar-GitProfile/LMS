using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Repo.Models
{
    public class UserCourses
    {   
        public int UserCourseId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string CourseName { get; set; }
        public string description { get; set; }
        public string ImageUrl { get; set; }
        public int QuizScore { get; set; }
    }
}
