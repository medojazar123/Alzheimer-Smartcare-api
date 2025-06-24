using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Enums;

namespace api.DTOs.Account
{
    // Update DTO
    public class UpdateReminderDto
    {
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Body { get; set; }
        
        [Required]
        public DateTime ScheduledTime { get; set; }
        
        public string Repeat { get; set; } = "None";
        
        [Required]
        public string FcmToken { get; set; }
    }
}
