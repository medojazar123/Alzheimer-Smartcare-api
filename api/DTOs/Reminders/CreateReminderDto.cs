using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Enums;

namespace api.DTOs.Account
{
    // Enhanced Create DTO with validation
    public class CreateReminderDto
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Title { get; set; }
        
        [Required]
        [StringLength(500, MinimumLength = 1)]
        public string Body { get; set; }
        
        [Required]
        public DateTime ScheduledTime { get; set; }
        
        public string Repeat { get; set; } = "None";
        
        [Required]
        public string FcmToken { get; set; }
    }
}
