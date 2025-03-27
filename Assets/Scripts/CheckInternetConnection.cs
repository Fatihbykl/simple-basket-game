using System.Collections;
using UnityEngine;

public class CheckInternetConnection : MonoBehaviour
{
    public GameObject panel;
    
    private static CheckInternetConnection instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        StartCoroutine(CheckConnection());
    }

    private IEnumerator CheckConnection()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Time.timeScale = 0;
                panel.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                panel.SetActive(false);
            }
        }
        
    }
}
