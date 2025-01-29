using UnityEngine;
using Unity.Notifications.Android;
using UnityEngine.Android;
using Unity.Notifications.iOS;

public class NotificationManager : MonoBehaviour{

    private void OnApplicationFocus(bool focus){
        if (focus == false){
            AndroidNotificationCenter.CancelAllDisplayedNotifications();

            iOSNotificationCenter.RemoveAllScheduledNotifications();
        }
    }
}
