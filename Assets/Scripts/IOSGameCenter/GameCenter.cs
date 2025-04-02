using System;
using System.Threading.Tasks;
using UnityEngine;

namespace IOSGameCenter
{
    public class AppleGameCenterExampleScript : MonoBehaviour
    {
        void Start()
        {
            AuthenticateUser();
        }

        void AuthenticateUser()
        {
            Social.localUser.Authenticate(success => {
                if (success)
                    Debug.Log("Game Center Giriş Başarılı: " + Social.localUser.userName);
                else
                    Debug.Log("Game Center Girişi Başarısız!");
            });
        }
    }
}
