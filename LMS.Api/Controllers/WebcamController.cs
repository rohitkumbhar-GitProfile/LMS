using LMS.Services.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace LMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebcamController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public WebcamController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage([FromBody] WebcamImageDto imageDto)
        {
            if (string.IsNullOrWhiteSpace(imageDto.ImageBase64))
                return BadRequest("Image data is empty.");

            // Strip the data URL prefix if it exists
            var base64Data = Regex.Replace(imageDto.ImageBase64, @"^data:image\/[a-zA-Z]+;base64,", string.Empty);
            byte[] imageBytes;

            try
            {
                imageBytes = Convert.FromBase64String(base64Data);
            }
            catch
            {
                return BadRequest("Invalid base64 image.");
            }

            var fileName = $"webcam_{DateTime.UtcNow:yyyyMMdd_HHmmss}.jpg";
            var folderPath = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var filePath = Path.Combine(folderPath, fileName);
            await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);

            return Ok(new { Message = "Image received and saved.", File = fileName });
        }
    }
}
