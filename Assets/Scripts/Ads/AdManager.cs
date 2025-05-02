using GoogleMobileAds.Api;
using UnityEngine;

namespace Ads
{
    public class AdManager : MonoBehaviour
    {
        public RewardedAds rewardAd;
        public InterstitialAds interstitialAds;
        public BannerAd bannerAd;
        public GameObject ContinueAfterAdScreen;
        
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

        public void ShowInterstitialAd()
        {
            Time.timeScale = 0;
            interstitialAds.ShowAd();
            ContinueAfterAdScreen.SetActive(true);
        }

        public void ThreeHealthAd()
        {
            rewardAd.ShowAd(RewardAdType.ThreeHealth);
            ContinueAfterAdScreen.SetActive(true);
        }

        public void DoubleScoreAd()
        {
            rewardAd.ShowAd(RewardAdType.DoubleScorePowerUp);
            ContinueAfterAdScreen.SetActive(true);
        }

        public void TinyBallAd()
        {
            rewardAd.ShowAd(RewardAdType.TinyBallPowerUp);
            ContinueAfterAdScreen.SetActive(true);
        }

        public void ShieldAd()
        {
            rewardAd.ShowAd(RewardAdType.ShieldPowerUp);
            ContinueAfterAdScreen.SetActive(true);
        }

        public void ContinueAfterAd()
        {
            ContinueAfterAdScreen.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
