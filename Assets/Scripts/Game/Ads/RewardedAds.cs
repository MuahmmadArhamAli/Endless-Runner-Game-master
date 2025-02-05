using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
// using GoogleMobileAds.Api;

public class RewardedAds : MonoBehaviour ,IUnityAdsLoadListener ,IUnityAdsShowListener
{
    [SerializeField] private string androidAdUnitId;
    [SerializeField] private string iosAdUnitId;
    private string adUnitId;

    #region Google Ads
    // private RewardedAd rewardedAd;
    #endregion

    private void Awake()
    {
        #if UNITY_IOS
                adUnitId = iosAdUnitId;
        #elif UNITY_ANDROID
                adUnitId = androidAdUnitId;
        #else
            adUnitId = androidAdUnitId;
        #endif
    }


    public void LoadRewardedAd()
    {
        #region Unity Ads
        Debug.Log(adUnitId);
        Advertisement.Load(adUnitId, this);
        #endregion

        #region  Google Ads

        // if (rewardedAd != null)
        // {
        //     rewardedAd.Destroy();
        //     rewardedAd = null;
        // }

        // Debug.Log("Loading the rewarded ad.");

        // // create our request used to load the ad.
        // var adRequest = new AdRequest();

        // // send the request to load the ad.
        // RewardedAd.Load(adUnitId, adRequest,
        //     (RewardedAd ad, LoadAdError error) =>
        //     {
        //         // if error is not null, the load request failed.
        //         if (error != null || ad == null)
        //         {
        //             Debug.LogError("Rewarded ad failed to load an ad " +
        //                            "with error : " + error);
        //             return;
        //         }
        //         Debug.Log("Rewarded ad loaded with response : "
        //                   + ad.GetResponseInfo());
        //         rewardedAd = ad;
        //     });

        #endregion

    }

    public void ShowRewardedAd()
    {
        #region Unity Ads

        Advertisement.Show(adUnitId, this);
        LoadRewardedAd();
        
        #endregion

        #region Google Ads
    
        // if (rewardedAd != null && rewardedAd.CanShowAd())
        // {
        //     rewardedAd.Show((Reward reward) =>
        //     {
        //         // TODO: Reward the user.
        //         PlayerPrefs.SetInt("TotalGems", PlayerPrefs.GetInt("TotalGems", 0) + 200);

        //         rewardedAd.Destroy();

        //         LoadRewardedAd();
        //     });
        // }
        
        #endregion

    }

    #region LoadCallbacks
    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Rewarded Ad Loaded");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message) { }
    #endregion

    #region ShowCallbacks
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) { }

    public void OnUnityAdsShowStart(string placementId) { }

    public void OnUnityAdsShowClick(string placementId) { }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId == adUnitId && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Ads Fully Watched .....");
            PlayerPrefs.SetInt("TotalGems", PlayerPrefs.GetInt("TotalGems", 0) + 200);
        }
    }
    #endregion


    #region Google Ads

//     private void RegisterEventHandlers(RewardedAd ad)
// {
//     // Raised when the ad is estimated to have earned money.
//     ad.OnAdPaid += (AdValue adValue) =>
//     {

//     };
//     // Raised when an impression is recorded for an ad.
//     ad.OnAdImpressionRecorded += () =>
//     {
//         Debug.Log("Rewarded ad recorded an impression.");
//     };
//     // Raised when a click is recorded for an ad.
//     ad.OnAdClicked += () =>
//     {
//         Debug.Log("Rewarded ad was clicked.");
//     };
//     // Raised when an ad opened full screen content.
//     ad.OnAdFullScreenContentOpened += () =>
//     {
//         Debug.Log("Rewarded ad full screen content opened.");
//     };
//     // Raised when the ad closed full screen content.
//     ad.OnAdFullScreenContentClosed += () =>
//     {
//         Debug.Log("Rewarded ad full screen content closed.");
//     };
//     // Raised when the ad failed to open full screen content.
//     ad.OnAdFullScreenContentFailed += (AdError error) =>
//     {
//         Debug.LogError("Rewarded ad failed to open full screen content " +
//                        "with error : " + error);
//     };
// }

    #endregion


}