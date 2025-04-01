using System;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Ads
{
    public class AdManager : MonoBehaviour
    {
        public RewardedAds rewardAd;
        public InterstitialAds interstitialAds;
        public BannerAd bannerAd;
        
        public static AdManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            MobileAds.Initialize(initStatus =>
            {
                rewardAd.LoadAd();
                interstitialAds.LoadAd();
                bannerAd.LoadAd();
                Debug.Log("AdMob Initialized.");
            });
        }

        public void ThreeHealthAd()
        {
            rewardAd.ShowAd(RewardAdType.ThreeHealth);
        }

        public void DoubleScoreAd()
        {
            rewardAd.ShowAd(RewardAdType.DoubleScorePowerUp);
        }

        public void TinyBallAd()
        {
            rewardAd.ShowAd(RewardAdType.TinyBallPowerUp);
        }

        public void ShieldAd()
        {
            rewardAd.ShowAd(RewardAdType.ShieldPowerUp);
        }
    }
}
