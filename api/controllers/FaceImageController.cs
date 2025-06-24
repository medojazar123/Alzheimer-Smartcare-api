using api.Mappers;
using api.DTOs.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using api.interfaces;
using api.models;
using System.Security.Claims;

namespace api.controllers
{
    [Route("api/FaceImages")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IFaceImageRepository _faceImageRepo;

        public ImageController(IFaceImageRepository faceImageRepo)
        {
            _faceImageRepo = faceImageRepo;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile image, [FromForm] string name, [FromForm] string userEmail)
        {
            if (image == null || image.Length == 0 || string.IsNullOrEmpty(userEmail) || string.IsNullOrEmpty(name))
                return BadRequest("Missing data.");

            using var ms = new MemoryStream();
            await image.CopyToAsync(ms);
            var imageBytes = ms.ToArray();
            var base64String = Convert.ToBase64String(imageBytes);

            var faceImage = new FaceImage
            {
                Name = name,
                UserEmail = userEmail,
                Base64Image = base64String
            };

            await _faceImageRepo.AddFaceImageAsync(faceImage);

            return Ok(new { Message = "Image uploaded." });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetImages()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email || c.Type == "email")?.Value;

            if (string.IsNullOrEmpty(email))
                return Unauthorized("Email not found in token.");

            var images = await _faceImageRepo.GetImagesByUserEmailAsync(email);
            var result = images.Select(i => i.ToDto()).ToList();

            return Ok(result);
        }
    }
}