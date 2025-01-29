using UnityEngine;
#if UNITY_ANDROID
using Unity.Notifications.Android;
using UnityEngine.Android;
#endif

#if UNITY_IOS
using Unity.Notifications.iOS;
#endif

public class NotificationManager : MonoBehaviour{

    private void OnApplicationFocus(bool focus){
        if (focus == false){

            #if UNITY_ANDROID
            AndroidNotificationCenter.CancelAllDisplayedNotifications();
            #endif 

            #if UNITY_IOS
            iOSNotificationCenter.RemoveAllScheduledNotifications();
            #endif
        }
    }
}
