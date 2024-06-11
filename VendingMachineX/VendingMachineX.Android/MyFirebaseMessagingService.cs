using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Firebase.Messaging;
using Android.Graphics;
using AndroidX.Core.App;

namespace VendingMachineX.Droid
{
    [Service(Name = "com.companyname.vendingmachinex.MyFirebaseMessagingService")]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        private readonly String NOTIFICATION_CHANNEL_ID = "200";
        public override void OnMessageReceived(RemoteMessage message)
        {
            SendNotification(message.GetNotification().Title, message.GetNotification().Body);
        }
        private void SendNotification(IDictionary<string, string> data)
        {
            string title, body;
            data.TryGetValue("title", out title);
            data.TryGetValue("body", out body);

            SendNotification("title", "body");
        }

        private void SendNotification(string title, string body)
        {
            NotificationManager notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                NotificationChannel notificationChannel = new NotificationChannel(NOTIFICATION_CHANNEL_ID, "Notification_Channel",
                    Android.App.NotificationImportance.Max);
                notificationChannel.Description = "";
                notificationChannel.EnableLights(true);
                notificationChannel.LightColor = Color.Blue;
                notificationChannel.SetVibrationPattern(new long[] { 0, 1000, 500, 1000 });
                notificationManager.CreateNotificationChannel(notificationChannel);
            }

            NotificationCompat.Builder notificationBuilder = new NotificationCompat.Builder(this, NOTIFICATION_CHANNEL_ID);

            notificationBuilder.SetAutoCancel(true)
                .SetDefaults(-1)
                .SetWhen(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds())
                .SetContentTitle(title)
                .SetContentText(body)
                .SetContentInfo("info")
                .SetSmallIcon(Resource.Drawable.notificacion);
            notificationManager.Notify(new Random().Next(), notificationBuilder.Build());
        }
    }
}