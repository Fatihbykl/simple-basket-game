using System;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Ads
{
    public class BannerAd : MonoBehaviour
    {
        #if UNITY_ANDROID
                private string _adUnitId = Keys.BANNER_AD_ANDROID;
        #elif UNITY_IPHONE
                private string _adUnitId = Keys.BANNER_AD_IOS;
        #else
                private string _adUnitId = "unused";
        #endif

        private BannerView _bannerView;


        private void Start()
        {
            LoadAd();
        }

        /// <summary>
        /// Creates a 320x50 banner view at bottom of the screen.
        /// </summary>
        public void CreateBannerView()
        {
            Debug.Log("Creating banner view");

            // If we already have a banner, destroy the old one.
            if (_bannerView != null)
            {
                DestroyAd();
            }

            // Create a 320x50 banner at top of the screen
            _bannerView = new BannerView(_adUnitId, AdSize.Banner, AdPosition.Bottom);
        }
    
        public void LoadAd()
        {
            // create an instance of a banner view first.
            if(_bannerView == null)
            {
                CreateBannerView();
            }

            // create our request used to load the ad.
            var adRequest = new AdRequest();

            // send the request to load the ad.
            Debug.Log("Loading banner ad.");
            _bannerView.LoadAd(adRequest);
        }
    
        public void DestroyAd()
        {
            if (_bannerView != null)
            {
                Debug.Log("Destroying banner view.");
                _bannerView.Destroy();
                _bannerView = null;
            }
        }
    }
}
