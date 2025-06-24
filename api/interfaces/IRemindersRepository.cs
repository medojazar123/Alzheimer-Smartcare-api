using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Account;
using api.models;

namespace api.interfaces
{
    public interface IRemindersRepository
    {
        Task<IEnumerable<MedicineReminder>> GetAllAsync();
        Task<MedicineReminder?> GetByIdAsync(int id);
        Task<IEnumerable<MedicineReminder>> GetByFcmTokenAsync(string fcmToken);
        Task<IEnumerable<MedicineReminder>> GetUpcomingRemindersAsync(DateTime fromTime);
        Task<MedicineReminder> CreateAsync(MedicineReminder reminder);
        Task<MedicineReminder?> UpdateAsync(int id, MedicineReminder reminder);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}