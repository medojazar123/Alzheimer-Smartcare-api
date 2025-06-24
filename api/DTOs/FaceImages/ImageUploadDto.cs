using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Enums;

namespace api.DTOs.Account
{
    public class UploadFaceImageDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Base64Image { get; set; }
        [Required]
        [EmailAddress]
        public string UserEmail { get; set; }
    }
}