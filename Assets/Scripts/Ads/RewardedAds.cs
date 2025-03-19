using System;
using GoogleMobileAds.Api;
using TigerForge;
using UnityEngine;

namespace Ads
{
    public class RewardedAds : MonoBehaviour
    {
#if UNITY_ANDROID
        private string _adUnitId = Keys.REWARDED_AD_ANDROID;
#elif UNITY_IPHONE
      private string _adUnitId = Keys.REWARDED_AD_IOS;
#else
      private string _adUnitId = "unused";
#endif
    
        private RewardedAd _rewardedAd;
    
        /// <summary>
        /// Loads the rewarded ad.
        /// </summary>
        public void LoadRewardedAd()
        {
            // Clean up the old ad before loading a new one.
            if (_rewardedAd != null)
            {
                _rewardedAd.Destroy();
                _rewardedAd = null;
            }
    
            Debug.Log("Loading the rewarded ad.");
    
            // create our request used to load the ad.
            var adRequest = new AdRequest();
    
            // send the request to load the ad.
            RewardedAd.Load(_adUnitId, adRequest,
                (RewardedAd ad, LoadAdError error) =>
                {
                    // if error is not null, the load request failed.
                    if (error != null || ad == null)
                    {
                        Debug.LogError("Rewarded ad failed to load an ad " +
                                       "with error : " + error);
                        return;
                    }
    
                    Debug.Log("Rewarded ad loaded with response : "
                              + ad.GetResponseInfo());
    
                    _rewardedAd = ad;
                });
        }
        public void ShowRewardedAd()
        {
            LoadRewardedAd();
            const string rewardMsg = "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

            if (_rewardedAd != null && _rewardedAd.CanShowAd())
            {
                _rewardedAd.Show((Reward reward) =>
                {
                    EventManager.EmitEvent(EventNames.Revived);
                    Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
                });
            }
        }
    }
}
