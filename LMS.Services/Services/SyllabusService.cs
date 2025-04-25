using LMS.Services.DTOs;
using LMS.Services.IServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Services.Services
{
    public class SyllabusService:ISyllabusService
    {
        private readonly IAIClient _aiClient;

        public SyllabusService(IAIClient aiClient)
        {
            _aiClient = aiClient;
        }

        public async Task<SyllabusResponse> GetSyllabusAsync(string courseName, bool isList=false)
        {
            try
            {
                var syllabus = await _aiClient.GetSyllabusAsync(courseName);
                //if (isList)
                //{
                //    var vardata = 
                //}

                return new SyllabusResponse
                {
                    CourseName = courseName,
                    CourseList = isList == true ? JsonConvert.DeserializeObject<List<Courses>>(syllabus).ToList() : new List<Courses>(),
                    Syllabus=syllabus
                };
            }
            catch (Exception)
            {
                
                return new SyllabusResponse();
            }
        }
    }
}
