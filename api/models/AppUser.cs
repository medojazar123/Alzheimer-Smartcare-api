using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Enums;
using Microsoft.AspNetCore.Identity;

namespace api.models
{
    public class AppUser :IdentityUser
    {
    [Required]
    public string? UserType { get; set; }
    [Required]
    public string? FullName { get; set; }
    }
}