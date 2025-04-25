using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Services.DTOs
{
    public class UserQuizScoreDto
    {
        public int UserQuizId { get; set; }
        public int UserId { get; set; }
        public string CourseName { get; set; }
        public string QuizType { get; set; }
        public int Score { get; set; }
        public int TotalScore { get; set; }
        public int AttemptedOn { get; set; }
    }
}
