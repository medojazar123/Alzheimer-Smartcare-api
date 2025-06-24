using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.interfaces;
using api.models;
using Microsoft.EntityFrameworkCore;
using PostgreSQL.Data;

namespace api.Repository
{
    public class RemindersRepository : IRemindersRepository
    {
        private readonly AppDbContext _context;

        public RemindersRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MedicineReminder>> GetAllAsync()
        {
            return await _context.MedicineReminders
                .OrderBy(r => r.ScheduledTime)
                .ToListAsync();
        }

        public async Task<MedicineReminder?> GetByIdAsync(int id)
        {
            return await _context.MedicineReminders
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<MedicineReminder>> GetByFcmTokenAsync(string fcmToken)
        {
            return await _context.MedicineReminders
                .Where(r => r.FcmToken == fcmToken)
                .OrderBy(r => r.ScheduledTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<MedicineReminder>> GetUpcomingRemindersAsync(DateTime fromTime)
        {
            return await _context.MedicineReminders
                .Where(r => r.ScheduledTime >= fromTime)
                .OrderBy(r => r.ScheduledTime)
                .ToListAsync();
        }

        public async Task<MedicineReminder> CreateAsync(MedicineReminder reminder)
        {
            await _context.MedicineReminders.AddAsync(reminder);
            await _context.SaveChangesAsync();
            return reminder;
        }

        public async Task<MedicineReminder?> UpdateAsync(int id, MedicineReminder reminder)
        {
            var existingReminder = await _context.MedicineReminders
                .FirstOrDefaultAsync(r => r.Id == id);

            if (existingReminder == null)
                return null;

            existingReminder.Title = reminder.Title;
            existingReminder.Body = reminder.Body;
            existingReminder.ScheduledTime = reminder.ScheduledTime;
            existingReminder.Repeat = reminder.Repeat;
            existingReminder.FcmToken = reminder.FcmToken;

            await _context.SaveChangesAsync();
            return existingReminder;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var reminder = await _context.MedicineReminders
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reminder == null)
                return false;

            _context.MedicineReminders.Remove(reminder);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.MedicineReminders
                .AnyAsync(r => r.Id == id);
        }
    }
}