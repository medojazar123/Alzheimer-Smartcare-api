using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using api.Enums;
using api.Interfaces;
using api.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PostgreSQL.Data;

namespace api.Services
{
public class ReminderNotificationService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _fcmServerKey;

    public ReminderNotificationService(IServiceScopeFactory scopeFactory, IHttpClientFactory httpClientFactory , IConfiguration config)
    {
        _scopeFactory = scopeFactory;
        _httpClientFactory = httpClientFactory;
        _fcmServerKey = config["Fcm:ServerKey"];
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var now = DateTime.UtcNow;
            var dueReminders = await context.MedicineReminders
                .Where(r =>
                    r.ScheduledTime <= now &&
                    (r.Repeat == ReminderRepeatType.none || r.Repeat == ReminderRepeatType.daily || 
                     (r.Repeat == ReminderRepeatType.weekly && r.ScheduledTime.DayOfWeek == now.DayOfWeek) ||
                     (r.Repeat == ReminderRepeatType.monthly && r.ScheduledTime.Day == now.Day)))
                .ToListAsync();

            foreach (var reminder in dueReminders)
            {
                await SendFcmNotification(reminder.Title, reminder.Body, reminder.FcmToken);

                if (reminder.Repeat == ReminderRepeatType.none)
                {
                    context.MedicineReminders.Remove(reminder);
                }
                else
                {
                    // update next schedule
                    switch (reminder.Repeat)
                    {
                        case ReminderRepeatType.daily:
                            reminder.ScheduledTime = reminder.ScheduledTime.AddDays(1);
                            break;
                        case ReminderRepeatType.weekly:
                            reminder.ScheduledTime = reminder.ScheduledTime.AddDays(7);
                            break;
                        case ReminderRepeatType.monthly:
                            reminder.ScheduledTime = reminder.ScheduledTime.AddMonths(1);
                            break;
                    }
                }
            }

            await context.SaveChangesAsync();
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }

    private async Task SendFcmNotification(string title, string body, string token)
    {
        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"key={_fcmServerKey}");

        var payload = new
        {
            to = token,
            notification = new { title, body },
            data = new { type = "reminder" }
        };

        var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
        await client.PostAsync("https://fcm.googleapis.com/fcm/send", content);
    }
}
}