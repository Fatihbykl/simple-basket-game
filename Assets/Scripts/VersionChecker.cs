using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class VersionChecker : MonoBehaviour
{
    private string _versionCheckUrl = "https://fatihbykl.github.io/blitz-basket-website/min_versions.json";

    void Start()
    {
        if (ShouldCheckVersionToday())
        {
            StartCoroutine(CheckVersion());
        }
        else
        {
            Debug.Log("No need to check version.");
        }
    }

    IEnumerator CheckVersion()
    {
        UnityWebRequest www = UnityWebRequest.Get(_versionCheckUrl);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            var json = www.downloadHandler.text;
            var minVersion = JsonUtility.FromJson<VersionData>(json);

            string requiredVersion = minVersion.minimum_version_android;
            
            if (IsVersionOlder(Application.version, requiredVersion))
            {
                Debug.LogWarning("Need update!");
                Application.OpenURL("https://play.google.com/store/apps/details?id=com.RodeaGames.BlitzBasket");
            }
            else
            {
                Debug.Log("No need update!");
                SaveVersionCheckData();
            }
        }
        else
        {
            Debug.LogError("Version checker error: " + www.error);
        }
    }

    bool IsVersionOlder(string current, string required)
    {
        var currentParts = current.Split('.');
        var requiredParts = required.Split('.');

        for (int i = 0; i < Mathf.Max(currentParts.Length, requiredParts.Length); i++)
        {
            int cur = (i < currentParts.Length) ? int.Parse(currentParts[i]) : 0;
            int req = (i < requiredParts.Length) ? int.Parse(requiredParts[i]) : 0;

            if (cur < req)
                return true;
            else if (cur > req)
                return false;
        }
        return false;
    }

    bool ShouldCheckVersionToday()
    {
        string lastDate = PlayerPrefs.GetString("version_check_date", "");
        string lastVersion = PlayerPrefs.GetString("version_checked_app_version", "");
        string today = System.DateTime.Now.ToString("yyyyMMdd");

        return lastDate != today || lastVersion != Application.version;
    }

    void SaveVersionCheckData()
    {
        string today = System.DateTime.Now.ToString("yyyyMMdd");
        PlayerPrefs.SetString("version_check_date", today);
        PlayerPrefs.SetString("version_checked_app_version", Application.version);
        PlayerPrefs.Save();
    }

    [System.Serializable]
    public class VersionData
    {
        public string minimum_version_android;
        public string minimum_version_ios;
    }
}
