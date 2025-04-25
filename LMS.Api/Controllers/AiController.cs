using LMS.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AiController : ControllerBase
    {
        private readonly ISyllabusService _syllabusService;

        public AiController(ISyllabusService syllabusService)
        {
            _syllabusService = syllabusService;
        }

        [HttpPost("CourseList")]
        public async Task<IActionResult> GetCourseList([FromBody] string courseName)
        {
            var result = await _syllabusService.GetSyllabusAsync(courseName,true);
            return Ok(result);
        }
        [HttpPost("MoreInfo")]
        public async Task<IActionResult> GetSyllabus([FromBody] string courseName)
        {
            var result = await _syllabusService.GetSyllabusAsync(courseName,false);
            return Ok(result);
        } 
        [HttpPost("Quiz")]
        public async Task<IActionResult> GetQuiz([FromBody] string courseName)
        {
            var result = await _syllabusService.GetSyllabusAsync(courseName,false);
            return Ok(result);
        }
    }
}
