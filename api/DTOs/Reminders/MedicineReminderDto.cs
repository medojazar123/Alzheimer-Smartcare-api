using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Enums;

namespace api.DTOs.Account
{
    // Response DTO
    public class MedicineReminderDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime ScheduledTime { get; set; }
        public string Repeat { get; set; }
        public string FcmToken { get; set; }
    }

}
