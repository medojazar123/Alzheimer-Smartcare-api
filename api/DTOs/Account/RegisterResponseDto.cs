using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Enums;

namespace api.DTOs.Account
{
    public class RegisterResponseDto
    {
        public string? Fullname { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? UserType { get; set; }
        public string? Token { get; set; }
    }
}