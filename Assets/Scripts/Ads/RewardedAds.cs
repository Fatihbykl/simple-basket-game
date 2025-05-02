using System;
using GoogleMobileAds.Api;
using Quests;
using TigerForge;
using UnityEngine;

namespace Ads
{
    [Serializable]
    public enum RewardAdType
    {
        ThreeHealth,
        ShieldPowerUp,
        DoubleScorePowerUp,
        TinyBallPowerUp
    }
    
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
        private RewardAdType _rewardAdType; 
        
        public static RewardedAds instance;

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
        }
    
        public void LoadAd()
        {
            if (_rewardedAd != null)
            {
                DestroyAd();
            }

            Debug.Log("Loading rewarded ad.");

            var adRequest = new AdRequest();

            RewardedAd.Load(_adUnitId, adRequest, (RewardedAd ad, LoadAdError error) =>
            {
                if (error != null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad with error : " + error);
                    return;
                }
                
                if (ad == null)
                {
                    Debug.LogError("Unexpected error: Rewarded load event fired with null ad and null error.");
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : " + ad.GetResponseInfo());
                _rewardedAd = ad;

                RegisterEventHandlers(ad);
            });
        }

        public void ShowAd(RewardAdType rewardAdType)
        {
            if (_rewardedAd != null && _rewardedAd.CanShowAd())
            {
                _rewardAdType = rewardAdType;
                _rewardedAd.Show((Reward reward) =>
                {
                    Debug.Log(String.Format("Rewarded ad granted a reward: {0} {1}",
                                            reward.Amount,
                                            reward.Type));
                });
            }
            else
            {
                Debug.LogError("Rewarded ad is not ready yet.");
            }

        }
        
        public void DestroyAd()
        {
            if (_rewardedAd != null)
            {
                Debug.Log("Destroying rewarded ad.");
                _rewardedAd.Destroy();
                _rewardedAd = null;
            }
        }
        public void LogResponseInfo()
        {
            if (_rewardedAd != null)
            {
                var responseInfo = _rewardedAd.GetResponseInfo();
                UnityEngine.Debug.Log(responseInfo);
            }
        }

        private void RegisterEventHandlers(RewardedAd ad)
        {
            ad.OnAdPaid += (AdValue adValue) =>
            {
                Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
                    adValue.Value,
                    adValue.CurrencyCode));
            };
            ad.OnAdFullScreenContentClosed += () =>
            {
                GrantReward();
                LoadAd();
                Time.timeScale = 0;
            };
            ad.OnAdFullScreenContentFailed += (AdError error) =>
            {
                Debug.LogError("Rewarded ad failed to open full screen content with error : "
                    + error);
            };
        }

        private void GrantReward()
        {
            switch (_rewardAdType)
            {
                case RewardAdType.ThreeHealth:
                    EventManager.EmitEventData(EventNames.Revived, 3);
                    break;
                case RewardAdType.ShieldPowerUp:
                    EventManager.EmitEvent(EventNames.AddShield);
                    break;
                case RewardAdType.DoubleScorePowerUp:
                    EventManager.EmitEvent(EventNames.AddDoubleScore);
                    break;
                case RewardAdType.TinyBallPowerUp:
                    EventManager.EmitEvent(EventNames.AddTinyBall);
                    break;
            }
        }
    }
}
