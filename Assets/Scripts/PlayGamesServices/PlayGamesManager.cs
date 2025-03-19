using GooglePlayGames;
using UnityEngine;

public class PlayGamesManager : MonoBehaviour
{
    void Start()
    {
        PlayGamesPlatform.Activate();
        SignIn();
    }

    void SignIn()
    {
        PlayGamesPlatform.Instance.localUser.Authenticate((bool success) => {
            if (success)
            {
                Debug.Log("Google Play Hizmetlerine giriş başarılı!");
            }
            else
            {
                Debug.Log("Google Play Hizmetlerine giriş başarısız.");
            }
        });
    }
    
    public void SubmitScore(int score)
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ReportScore(score, Keys.LEADERBOARD_ID, (bool success) => {
                if (success)
                {
                    Debug.Log("Skor başarıyla gönderildi!");
                }
                else
                {
                    Debug.Log("Skor gönderme başarısız.");
                }
            });
        }
    }
    
    public void ShowLeaderboard()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI(Keys.LEADERBOARD_ID);
        }
    }


}
