using Cysharp.Threading.Tasks;
using DG.Tweening;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        public GameObject continueScreenBg;
        public GameObject continueScreenRay;
        public GameObject continueScreenPanel;
        public GameObject gameOverScreen;
        public GameObject gameOverTransparent;

        private void Start()
        {
            EventManager.StartListening(EventNames.NoHealth, OnNoHealth);
            EventManager.StartListening(EventNames.Revived, OnRevived);
        }

        private void OnRevived()
        {
            AnimateContinueScreenMoveOut();
        }

        private void OnNoHealth()
        {
            AnimateContinueScreenMoveIn();
        }

        public void ClosedContinueScreen()
        {
            EventManager.EmitEvent(EventNames.GameOver);
            AnimateContinueScreenMoveOut();
            AnimateScreenMoveIn();
        }

        private async UniTask AnimateContinueScreenMoveIn()
        {
            //await UniTask.WaitForSeconds(1f, true);
            await UniTask.WhenAll(
                continueScreenPanel.transform.DOLocalMoveX(0f, 0.5f).SetUpdate(true).ToUniTask(),
                continueScreenBg.GetComponent<Image>().DOFade(100f / 255f, 0.5f).SetUpdate(true).ToUniTask(),
                continueScreenRay.GetComponent<Image>().DOFade(30f / 255f, 0.5f).SetUpdate(true).ToUniTask()
            );

        }

        private async UniTask AnimateContinueScreenMoveOut()
        {
            await UniTask.WhenAll(
                continueScreenPanel.transform.DOLocalMoveX(-1500f, 0.5f).SetUpdate(true).ToUniTask(),
                continueScreenBg.GetComponent<Image>().DOFade(0f / 255f, 0.5f).SetUpdate(true).ToUniTask(),
                continueScreenRay.GetComponent<Image>().DOFade(0f / 255f, 0.5f).SetUpdate(true).ToUniTask()
            );

        }

        private async UniTask AnimateScreenMoveIn()
        {
            await UniTask.WhenAll(
                gameOverScreen.transform.DOLocalMoveX(0f, 0.5f).SetUpdate(true).ToUniTask(),
                gameOverTransparent.GetComponent<Image>().DOFade(200f / 255f, 0.5f).SetUpdate(true).ToUniTask()
            );

        }
    
        private async UniTask AnimateScreenMoveOut()
        {
            await UniTask.WhenAll(
                gameOverScreen.transform.DOLocalMoveX(-1500f, 0.5f).SetUpdate(true).ToUniTask(),
                gameOverTransparent.GetComponent<Image>().DOFade(0f, 0.5f).SetUpdate(true).ToUniTask()
            );
        }
    }
}
