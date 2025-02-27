using System;
using TigerForge;
using TMPro;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private int health;

    private void Start()
    {
        EventManager.StartListening(EventNames.BallFell, OnBallFell);
        UpdateHealthUI();
    }

    private void OnBallFell()
    {
        health--;
        if (health <= 0)
        {
            Debug.Log("Game Over!");
        }
        
        UpdateHealthUI();
        transform.gameObject.transform.position = new Vector3(0, 4f, 0);
    }

    private void UpdateHealthUI()
    {
        healthText.text = health.ToString();
    }

    private void OnDestroy()
    {
        EventManager.StopListening(EventNames.BallFell, OnBallFell);
    }
}
