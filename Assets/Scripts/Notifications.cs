using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

public class Notifications : MonoBehaviour
{
    void Start()
    {
        ScheduleDailyNotification();
    }

    void ScheduleDailyNotification()
    {
        if (PlayerPrefs.GetInt("DailyNotificationScheduled", 0) == 1)
        {
            Debug.Log("Notification already scheduled.");
            return;
        }
        
        string systemLanguage = Application.systemLanguage.ToString();
        
        Dictionary<string, string> localizedTexts = new Dictionary<string, string>()
        {
            {"Turkish", "Rakiplerin bu gün senden daha fazla rekor kırdı. Aylık sıralamada birinci olup birbirinden değerli hediyeleri kazanmak için şimdi oyuna gir!"},
            {"English", "Your competitors broke more records than you today. Enter the game now to win valuable gifts in the monthly ranking!"}
        };

        string notificationText = localizedTexts.ContainsKey(systemLanguage) ? localizedTexts[systemLanguage] : localizedTexts["English"];


        var channel = new AndroidNotificationChannel()
        {
            Id = "daily_reminder_channel",
            Name = "Daily Reminder",
            Importance = Importance.Default,
            Description = "Daily reminder notification"
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        var fireTime = System.DateTime.Now.AddDays(1).Date.AddHours(21);
        
        var notification = new AndroidNotification()
        {
            Title = "Play Basket Blitz!",
            Text = notificationText,
            SmallIcon = "default",
            LargeIcon = "default",
            FireTime = fireTime,
            RepeatInterval = new System.TimeSpan(24, 0, 0)
        };

        var notificationId = AndroidNotificationCenter.SendNotification(notification, "daily_reminder_channel");

        PlayerPrefs.SetInt("DailyNotificationScheduled", 1);
        PlayerPrefs.Save();
    }
}
