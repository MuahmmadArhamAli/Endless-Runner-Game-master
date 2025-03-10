using System;
using UnityEngine;

public class PaystackConverter : MonoBehaviour{

    public string lastQuitTime = "lastQuitTime";

    [SerializeField] private AndroidNotification androidNotification;
    [SerializeField] private IOSNotification iosNotification;

    private void Start() {
        DateTime currentDate = DateTime.Now;
        string currentDateString = currentDate.ToString();

        DateTime lastSavedDate = DateTime.Parse(PlayerPrefs.GetString(lastQuitTime, currentDateString));

        if (currentDate.Month == lastSavedDate.Month) {
            return;
        }

        int money = PlayerPrefs.GetInt("TotalGems", 0);
        PlayerPrefs.SetInt("NGN",money);
        PlayerPrefs.SetInt("TotalGems", 0);
        PlayerPrefs.Save();

        Debug.Log(PlayerPrefs.GetInt("NGN", 0));

        int secondsToNextMonth = GetSecondsToNextMonth(currentDate);

#if UNITY_ANDOID
            androidNotification.SendNotification("Money Converted", "Your Gems Have been converted", secondsToNextMonth);
#elif UNITY_IOS
            iosNotification.SendNotification("Money Converted", "Your Gems Have been converted", "Withdraw them now", secondsToNextMonth);
#endif

    }


    private int GetSecondsToNextMonth(DateTime currentDate) {
        DateTime firstDayOfNextMonth = new DateTime(currentDate.Year,currentDate.Month,1).AddMonths(1);

        TimeSpan timeDifference = firstDayOfNextMonth - currentDate;
        return (int)timeDifference.TotalSeconds;
    }   
}
