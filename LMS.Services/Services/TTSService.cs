using LMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Services.Services
{
    public class TTSService : ITTSService
    {
        private readonly ITTSClient _ttsClient;
        private readonly ISyllabusService _syllabusService;

        public TTSService(ITTSClient ttsClient, ISyllabusService syllabusService)
        {
            _ttsClient = ttsClient;
            _syllabusService = syllabusService;
        }

        public async Task<byte[]> GetSpeechAsync(string text)
        {
            var res=await _syllabusService.GetSyllabusAsync(text);
            return await _ttsClient.ConvertTextToSpeechAsync("");
        }
    }
}
