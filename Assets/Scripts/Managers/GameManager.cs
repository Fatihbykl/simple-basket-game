using Cysharp.Threading.Tasks;
using DG.Tweening;
using TigerForge;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private void Start()
        {
            EventManager.StartListening(EventNames.NoHealth, OnNoHealth);
            EventManager.StartListening(EventNames.Revived, OnRevived);
        }

        private void OnNoHealth()
        {
            DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 0f, 1f).SetUpdate(true);
        }

        private async void OnRevived()
        {
            await UniTask.WaitForSeconds(0.1f, ignoreTimeScale:true);
            Debug.Log("Revived");
            Time.timeScale = 0;
        }

        private void OnDestroy()
        {
            EventManager.StopListening(EventNames.NoHealth, OnNoHealth);
            EventManager.StopListening(EventNames.Revived, OnRevived);
        }
    }
}
