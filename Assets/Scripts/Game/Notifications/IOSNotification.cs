using UnityEngine;
using System.Collections;
using Unity.Notifications.iOS;

public class IOSNotification : MonoBehaviour{
    public IEnumerator RequestAuthorization(){
        using var request = new AuthorizationRequest(AuthorizationOption.Alert | AuthorizationOption.Badge, true);
        while (!request.IsFinished){
            yield return null;
        }
    }


    public void SendNotification(string title, string text, string subtitle, int fireTimeInSeconds){
        var timeTrigger = new iOSNotificationTimeIntervalTrigger(){
            TimeInterval = new System.TimeSpan(0,0,fireTimeInSeconds),
            Repeats = false
        };

        var notification = new iOSNotification(){
            Identifier = "Ad ready",
            Title = title,
            Body = text,
            Subtitle = subtitle,
            ShowInForeground = true,
            ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Badge),
            CategoryIdentifier = "default_category",
            ThreadIdentifier = "thread1",
            Trigger = timeTrigger
        };

        iOSNotificationCenter.ScheduleNotification(notification);
    }
}
