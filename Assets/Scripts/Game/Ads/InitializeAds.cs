using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;


public class InitializeAds : MonoBehaviour
{
    public RewardedAds rewardedAds;
    public InterstitialAds interestialAds;

    private void Start()
    {
        rewardedAds.LoadRewardedAd();
        interestialAds.LoadInterstitialAd();
    }
}