using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

// using GoogleMobileAds;
// using GoogleMobileAds.Api;

public class InterstitialAds : MonoBehaviour , IUnityAdsLoadListener ,IUnityAdsShowListener
{
    [SerializeField] private string androidAdUnitId;
    [SerializeField] private string iosAdUnitId;

    private string adUnitId;

    #region Google Ads

    // private InterstitialAd interstitialAd;

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

    private void Start(){
        #region  Google Ad Mob
        
        // MobileAds.Initialize((InitializationStatus initstatus)=>{
            
        // });

        #endregion
    }

    public void LoadInterstitialAd()
    {
        #region  Unity Ads
        Advertisement.Load(adUnitId, this);
        #endregion

        #region Google Ads

        // var adRequest = new AdRequest();
        
        // // send the request to load the ad.
        // InterstitialAd.Load(adUnitId, adRequest,
        //     (InterstitialAd ad, LoadAdError error) =>
        //     {
        //         // if error is not null, the load request failed.
        //         if (error != null || ad == null)
        //         {
        //             Debug.LogError("interstitial ad failed to load an ad " +
        //                            "with error : " + error);
        //             return;
        //         }

        //         Debug.Log("Interstitial ad loaded with response : "
        //                   + ad.GetResponseInfo());

        //         interstitialAd = ad;
        //     });

        #endregion
    }

    public void ShowInterstitialAd()
    {
        #region Unity Ads
        Advertisement.Show(adUnitId, this);
        LoadInterstitialAd();
        #endregion

        #region Google Ads

        //  if (interstitialAd != null && interstitialAd.CanShowAd())
        // {
        //     Debug.Log("Showing interstitial ad.");
        //     interstitialAd.Show();

        //     LoadInterstitialAd();
        // }
        // else
        // {
        //     Debug.LogError("Interstitial ad is not ready yet.");
        // }

        #endregion

    }
    
    #region  Unity Ads
    
    
    #region LoadCallbacks
    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Interstitial Ad Loaded");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)    {    }
    #endregion
    #region ShowCallbacks
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)    {    }

    public void OnUnityAdsShowStart(string placementId)    {    }

    public void OnUnityAdsShowClick(string placementId)    {    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("Interstitial Ad Completed");
    }
    #endregion


    #endregion
}