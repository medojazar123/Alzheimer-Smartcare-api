using System.ComponentModel.DataAnnotations.Schema;
using api.Enums;

namespace api.models
{
    public class MedicineReminder
    {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public DateTime ScheduledTime { get; set; }
    public ReminderRepeatType Repeat { get; set; }
    public string FcmToken { get; set; }
    }
} 