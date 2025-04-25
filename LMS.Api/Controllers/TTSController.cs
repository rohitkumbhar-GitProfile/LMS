using LMS.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TTSController : ControllerBase
    {

        private readonly ITTSService _ttsService;

        public TTSController(ITTSService ttsService)
        {
            _ttsService = ttsService;
        }

        [HttpGet("TextToVoice")]
        public async Task<IActionResult> ConvertTextToSpeech([FromQuery] string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return BadRequest("Text cannot be empty.");
            }

            var audioData = await _ttsService.GetSpeechAsync(text);
            return File(audioData, "audio/mpeg", "speech.mp3"); // Correct content type and filename
        }
    }
}
