using UnityEngine;
using Unity.Notifications.Android;
using UnityEngine.Android;
using UnityEditor.Rendering;
public class AndroidNotification : MonoBehaviour{

    public void RequestAuthorization(){
        if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS")){
            Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
        }
    }

    public void RegisterNotificationChannel(){
        var channel = new AndroidNotificationChannel{
            Id = "default_channel",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Ad Available"
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    public void SendNotification(string title, string text, int fireTimeInSeconds){
        var notification = new Unity.Notifications.Android.AndroidNotification();
        notification.Title = title;
        notification.Text = text;
        notification.FireTime = System.DateTime.Now.AddSeconds(fireTimeInSeconds);

        AndroidNotificationCenter.SendNotification(notification, "default_channel");
    }
    
}
