using System;
using Android.App;
using Firebase.Iid;
using Android.Util;
using Firebase.Messaging;
using Android.Content;
using Android.Media;
using Android.Support.V4.App;

namespace OxincNotification.Droid
{
    [Service]
    public class MyFirebaseIIDService : FirebaseMessagingService
    {
        const string TAG = "MyFirebaseIIDService";
        public override void OnNewToken(String s)
        {
            Log.Debug("NEW_TOKEN", s);
        }

        public override void OnMessageReceived(RemoteMessage message)
        {
            base.OnMessageReceived(message);
            SendNotification(message.GetNotification().Body);
        }

        private void SendNotification(string body)
        {
            var intent = new Intent(this, typeof(MainActivity));

            intent.AddFlags(ActivityFlags.ClearTop);
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

            var defaultSoundURI = RingtoneManager.GetDefaultUri(RingtoneType.Notification);
            var notificationBuilder = new NotificationCompat.Builder(this).
                SetSmallIcon(Resource.Drawable.navigation_empty_icon)
                .SetContentTitle("Oninx - SchoolName")
                .SetContentText(body)
                .SetAutoCancel(true)
                .SetSound(defaultSoundURI)
                .SetContentIntent(pendingIntent);

            var notificationManager = NotificationManager.FromContext(this);
            notificationManager.Notify(0, notificationBuilder.Build());
        }
    }
}