using System;
using TMPro;
using UnityEngine;

public class FpsCounter : MonoBehaviour
{
    public TextMeshProUGUI fpsText;

    private float deltaTime = 0.0f;
    private float updateInterval = 1f; // 0.5 saniyede bir güncelle
    private float timer = 0f;

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        timer += Time.unscaledDeltaTime;

        if (timer >= updateInterval)
        {
            int fps = Mathf.CeilToInt(1.0f / deltaTime);
            fpsText.text = "FPS: " + fps;
            timer = 0f; // Zamanlayıcıyı sıfırla
        }
    }
}
