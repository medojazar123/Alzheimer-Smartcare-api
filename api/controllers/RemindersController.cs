using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Account;
using api.Enums;
using api.interfaces;
using api.Mappers;
using api.models;
using Microsoft.AspNetCore.Mvc;
using PostgreSQL.Data;

namespace api.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicineReminderController : ControllerBase
    {
        private readonly IRemindersRepository _reminderRepository;

        public MedicineReminderController(IRemindersRepository reminderRepository)
        {
            _reminderRepository = reminderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var reminders = await _reminderRepository.GetAllAsync();
                var reminderDtos = reminders.Select(r => r.ToMedicineReminderDto()).ToList();
                
                return Ok(reminderDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving reminders", error = ex.Message });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var reminder = await _reminderRepository.GetByIdAsync(id);
                
                if (reminder == null)
                {
                    return NotFound(new { message = "Reminder not found" });
                }
                
                return Ok(reminder.ToMedicineReminderDto());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the reminder", error = ex.Message });
            }
        }

        [HttpGet("by-token/{fcmToken}")]
        public async Task<IActionResult> GetByFcmToken([FromRoute] string fcmToken)
        {
            try
            {
                var reminders = await _reminderRepository.GetByFcmTokenAsync(fcmToken);
                var reminderDtos = reminders.Select(r => r.ToMedicineReminderDto()).ToList();
                
                return Ok(reminderDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving reminders", error = ex.Message });
            }
        }

        [HttpGet("upcoming")]
        public async Task<IActionResult> GetUpcomingReminders([FromQuery] DateTime? fromTime = null)
        {
            try
            {
                var startTime = fromTime ?? DateTime.UtcNow;
                var reminders = await _reminderRepository.GetUpcomingRemindersAsync(startTime);
                var reminderDtos = reminders.Select(r => r.ToMedicineReminderDto()).ToList();
                
                return Ok(reminderDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving upcoming reminders", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReminderDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Validate scheduled time is in the future
                if (createDto.ScheduledTime <= DateTime.UtcNow)
                {
                    return BadRequest(new { message = "Scheduled time must be in the future" });
                }

                // Validate and parse Repeat type
                if (!Enum.TryParse<ReminderRepeatType>(createDto.Repeat, true, out var repeatType))
                {
                    var validValues = string.Join(", ", Enum.GetNames<ReminderRepeatType>());
                    return BadRequest(new { message = $"Invalid repeat value. Valid values are: {validValues}" });
                }

                var reminderModel = createDto.ToMedicineReminderFromCreateDto();
                var createdReminder = await _reminderRepository.CreateAsync(reminderModel);
                
                return CreatedAtAction(
                    nameof(GetById), 
                    new { id = createdReminder.Id }, 
                    createdReminder.ToMedicineReminderDto()
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the reminder", error = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateReminderDto updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Validate scheduled time is in the future
                if (updateDto.ScheduledTime <= DateTime.UtcNow)
                {
                    return BadRequest(new { message = "Scheduled time must be in the future" });
                }

                var reminderModel = updateDto.ToMedicineReminderFromUpdateDto();
                var updatedReminder = await _reminderRepository.UpdateAsync(id, reminderModel);
                
                if (updatedReminder == null)
                {
                    return NotFound(new { message = "Reminder not found" });
                }
                
                return Ok(updatedReminder.ToMedicineReminderDto());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the reminder", error = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var deleted = await _reminderRepository.DeleteAsync(id);
                
                if (!deleted)
                {
                    return NotFound(new { message = "Reminder not found" });
                }
                
                return Ok(new { message = "Reminder deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the reminder", error = ex.Message });
            }
        }
    }
}