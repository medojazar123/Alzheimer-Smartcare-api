using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using api.Enums;
using api.Interfaces;
using api.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using api.interfaces;

namespace api.Services
{
    public class ReminderNotificationService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ReminderNotificationService> _logger;
        private readonly string _fcmServerKey;
        private readonly string _fcmUrl = "https://fcm.googleapis.com/fcm/send";
        private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(1);
        private readonly TimeSpan _toleranceWindow = TimeSpan.FromMinutes(2);

        public ReminderNotificationService(
            IServiceScopeFactory scopeFactory, 
            IHttpClientFactory httpClientFactory,
            ILogger<ReminderNotificationService> logger,
            IConfiguration config)
        {
            _scopeFactory = scopeFactory;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _fcmServerKey = config["Fcm:ServerKey"] ?? throw new ArgumentNullException("FCM Server Key not configured");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Reminder Notification Service started");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await ProcessDueReminders(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while processing reminders");
                }

                try
                {
                    await Task.Delay(_checkInterval, stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }

            _logger.LogInformation("Reminder Notification Service stopped");
        }

        private async Task ProcessDueReminders(CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateScope();
            var reminderRepository = scope.ServiceProvider.GetRequiredService<IRemindersRepository>();

            var now = DateTime.UtcNow;
            var windowStart = now.Subtract(_toleranceWindow);
            var windowEnd = now.Add(_toleranceWindow);

            _logger.LogDebug("Checking for reminders between {WindowStart} and {WindowEnd}", windowStart, windowEnd);

            try
            {
                // Get reminders that are due within the tolerance window
                var dueReminders = await GetDueReminders(reminderRepository, windowStart, windowEnd);

                if (!dueReminders.Any())
                {
                    _logger.LogDebug("No due reminders found");
                    return;
                }

                _logger.LogInformation("Found {Count} due reminders", dueReminders.Count);

                var notificationTasks = dueReminders.Select(async reminder =>
                {
                    try
                    {
                        var success = await SendFcmNotification(reminder.Title, reminder.Body, reminder.FcmToken);
                        if (success)
                        {
                            _logger.LogInformation("Notification sent successfully for reminder {Id}", reminder.Id);
                            await ProcessReminderAfterNotification(reminderRepository, reminder, now);
                        }
                        else
                        {
                            _logger.LogWarning("Failed to send notification for reminder {Id}", reminder.Id);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error processing reminder {Id}", reminder.Id);
                    }
                });

                await Task.WhenAll(notificationTasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving due reminders");
            }
        }

        private async Task<List<MedicineReminder>> GetDueReminders(IRemindersRepository repository, DateTime windowStart, DateTime windowEnd)
        {
            var allReminders = await repository.GetUpcomingRemindersAsync(windowStart);
            var dueReminders = new List<MedicineReminder>();

            foreach (var reminder in allReminders)
            {
                if (IsReminderDue(reminder, windowStart, windowEnd))
                {
                    dueReminders.Add(reminder);
                }
            }

            return dueReminders;
        }

        private bool IsReminderDue(MedicineReminder reminder, DateTime windowStart, DateTime windowEnd)
        {
            var scheduledTime = reminder.ScheduledTime;

            // Check if the reminder is within the current time window
            if (scheduledTime >= windowStart && scheduledTime <= windowEnd)
            {
                return true;
            }

            // For repeating reminders, check if they match the current time pattern
            var now = DateTime.UtcNow;
            
            return reminder.Repeat switch
            {
                ReminderRepeatType.daily => 
                    scheduledTime.TimeOfDay >= windowStart.TimeOfDay && 
                    scheduledTime.TimeOfDay <= windowEnd.TimeOfDay,
                    
                ReminderRepeatType.weekly => 
                    scheduledTime.DayOfWeek == now.DayOfWeek &&
                    scheduledTime.TimeOfDay >= windowStart.TimeOfDay && 
                    scheduledTime.TimeOfDay <= windowEnd.TimeOfDay,
                    
                ReminderRepeatType.monthly => 
                    scheduledTime.Day == now.Day &&
                    scheduledTime.TimeOfDay >= windowStart.TimeOfDay && 
                    scheduledTime.TimeOfDay <= windowEnd.TimeOfDay,
                    
                _ => false
            };
        }

        private async Task ProcessReminderAfterNotification(IRemindersRepository repository, MedicineReminder reminder, DateTime now)
        {
            if (reminder.Repeat == ReminderRepeatType.none)
            {
                // Delete one-time reminders
                await repository.DeleteAsync(reminder.Id);
                _logger.LogInformation("One-time reminder {Id} deleted", reminder.Id);
            }
            else
            {
                // Update next schedule for repeating reminders
                var nextSchedule = CalculateNextSchedule(reminder.ScheduledTime, reminder.Repeat, now);
                reminder.ScheduledTime = nextSchedule;
                
                await repository.UpdateAsync(reminder.Id, reminder);
                _logger.LogInformation("Repeating reminder {Id} rescheduled to {NextSchedule}", reminder.Id, nextSchedule);
            }
        }

        private DateTime CalculateNextSchedule(DateTime currentSchedule, ReminderRepeatType repeatType, DateTime now)
        {
            return repeatType switch
            {
                ReminderRepeatType.daily => GetNextDailySchedule(currentSchedule, now),
                ReminderRepeatType.weekly => GetNextWeeklySchedule(currentSchedule, now),
                ReminderRepeatType.monthly => GetNextMonthlySchedule(currentSchedule, now),
                _ => currentSchedule
            };
        }

        private DateTime GetNextDailySchedule(DateTime currentSchedule, DateTime now)
        {
            var nextSchedule = currentSchedule.AddDays(1);
            
            // If we're behind schedule, catch up to the next future occurrence
            while (nextSchedule <= now)
            {
                nextSchedule = nextSchedule.AddDays(1);
            }
            
            return nextSchedule;
        }

        private DateTime GetNextWeeklySchedule(DateTime currentSchedule, DateTime now)
        {
            var nextSchedule = currentSchedule.AddDays(7);
            
            // If we're behind schedule, catch up to the next future occurrence
            while (nextSchedule <= now)
            {
                nextSchedule = nextSchedule.AddDays(7);
            }
            
            return nextSchedule;
        }

        private DateTime GetNextMonthlySchedule(DateTime currentSchedule, DateTime now)
        {
            var nextSchedule = currentSchedule.AddMonths(1);
            
            // Handle cases where the day doesn't exist in the next month (e.g., Jan 31 -> Feb 28)
            if (nextSchedule.Day != currentSchedule.Day)
            {
                nextSchedule = new DateTime(nextSchedule.Year, nextSchedule.Month, 
                    DateTime.DaysInMonth(nextSchedule.Year, nextSchedule.Month), 
                    nextSchedule.Hour, nextSchedule.Minute, nextSchedule.Second);
            }
            
            // If we're behind schedule, catch up to the next future occurrence
            while (nextSchedule <= now)
            {
                nextSchedule = nextSchedule.AddMonths(1);
                if (nextSchedule.Day != currentSchedule.Day)
                {
                    nextSchedule = new DateTime(nextSchedule.Year, nextSchedule.Month, 
                        DateTime.DaysInMonth(nextSchedule.Year, nextSchedule.Month), 
                        nextSchedule.Hour, nextSchedule.Minute, nextSchedule.Second);
                }
            }
            
            return nextSchedule;
        }

        private async Task<bool> SendFcmNotification(string title, string body, string token)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"key={_fcmServerKey}");

                var payload = new
                {
                    to = token,
                    notification = new 
                    { 
                        title = title,
                        body = body,
                        sound = "default",
                        badge = 1
                    },
                    data = new 
                    { 
                        type = "medicine_reminder",
                        timestamp = DateTime.UtcNow.ToString("O"),
                        priority = "high"
                    },
                    priority = "high",
                    content_available = true
                };

                var json = JsonConvert.SerializeObject(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                _logger.LogDebug("Sending FCM notification to token: {Token}", token[..Math.Min(token.Length, 10)] + "...");

                var response = await client.PostAsync(_fcmUrl, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogDebug("FCM notification sent successfully. Response: {Response}", responseContent);
                    return true;
                }
                else
                {
                    _logger.LogWarning("FCM notification failed. Status: {Status}, Response: {Response}", 
                        response.StatusCode, responseContent);
                    return false;
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Network error sending FCM notification");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error sending FCM notification");
                return false;
            }
        }
    }
}