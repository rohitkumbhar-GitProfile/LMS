using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Services.DTOs
{
    public class SyllabusResponse
    {
        public string CourseName { get; set; }
        public List<Courses> CourseList { get; set; }
        public string Syllabus { get; set; }
    }

   public class Courses
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string image { get; set; }

    }
}
