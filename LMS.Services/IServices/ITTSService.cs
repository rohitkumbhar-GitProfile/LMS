using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Services.IServices
{
    public interface ITTSService
    {
        Task<byte[]> GetSpeechAsync(string text);
    }
}
