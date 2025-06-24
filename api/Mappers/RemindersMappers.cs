using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Account;
using api.Enums;
using api.models;

namespace api.Mappers
{
public static class RemindersMappers
{
     public static MedicineReminderDto ToMedicineReminderDto(this MedicineReminder reminderModel)
        {
            return new MedicineReminderDto
            {
                Id = reminderModel.Id,
                Title = reminderModel.Title,
                Body = reminderModel.Body,
                ScheduledTime = reminderModel.ScheduledTime,
                Repeat = reminderModel.Repeat.ToString(),
                FcmToken = reminderModel.FcmToken
            };
        }

        public static MedicineReminder ToMedicineReminderFromCreateDto(this CreateReminderDto createDto)
        {
            // Parse the repeat string to enum
            if (!Enum.TryParse<ReminderRepeatType>(createDto.Repeat, true, out var repeatType))
            {
                repeatType = ReminderRepeatType.none; // Default value
            }

            return new MedicineReminder
            {
                Title = createDto.Title,
                Body = createDto.Body,
                ScheduledTime = createDto.ScheduledTime,
                Repeat = repeatType,
                FcmToken = createDto.FcmToken
            };
        }

        public static MedicineReminder ToMedicineReminderFromUpdateDto(this UpdateReminderDto updateDto)
        {
            // Parse the repeat string to enum
            if (!Enum.TryParse<ReminderRepeatType>(updateDto.Repeat, true, out var repeatType))
            {
                repeatType = ReminderRepeatType.none; // Default value
            }

            return new MedicineReminder
            {
                Title = updateDto.Title,
                Body = updateDto.Body,
                ScheduledTime = updateDto.ScheduledTime,
                Repeat = repeatType,
                FcmToken = updateDto.FcmToken
            };
        }
}
}