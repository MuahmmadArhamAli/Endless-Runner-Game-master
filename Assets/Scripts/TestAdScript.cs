using UnityEngine;

public class TestAdScript : MonoBehaviour
{
    public InterstitialAds interstitialAd;

    public AndroidNotification androidNotification;
    private float adTimer = 0f;

    private float convertTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        adTimer += Time.deltaTime;
        convertTimer += Time.deltaTime;
        if (adTimer > 60f){
            interstitialAd.ShowInterstitialAd();
            adTimer = 0f;
            androidNotification.RegisterNotificationChannel();            
            androidNotification.SendNotification("Ad Avaialable", "Watch Ad to Get reward", 60);
        }

        if (convertTimer >= 10 * 60f){
            convertTimer = 0f;

            int money = PlayerPrefs.GetInt("TotalGems", 0);
            PlayerPrefs.SetInt("NGN",money);
            PlayerPrefs.SetInt("TotalGems", 0);
            PlayerPrefs.Save();
            
            androidNotification.RegisterNotificationChannel();
            androidNotification.SendNotification("Money Converted", "Your Gems Have been converted", 10 * 60);
        }
    }
}
