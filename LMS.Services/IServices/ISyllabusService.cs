using LMS.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Services.IServices
{
    public interface ISyllabusService
    {
        Task<SyllabusResponse>GetSyllabusAsync(string courseName , bool isList=false);

    }
}
