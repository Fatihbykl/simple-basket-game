using GoogleMobileAds.Api;
using UnityEngine;

namespace Ads
{
    public class AdManager : MonoBehaviour
    {
        private void Start()
        {
            MobileAds.Initialize(initStatus => {Debug.Log("AdMob Initialized.");});
        }
    }
}
