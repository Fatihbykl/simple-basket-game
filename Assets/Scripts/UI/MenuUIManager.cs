using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class MenuUIManager : MonoBehaviour
    {
        public GameObject bgQuests;
        public GameObject bgBalls;
        public GameObject panelQuests;
        public GameObject panelBalls;

        public async void QuestsPanelIn()
        {
            bgQuests.SetActive(true);
            await UniTask.WhenAll(
                panelQuests.transform.DOLocalMoveX(0f, 0.5f).SetUpdate(true).ToUniTask(),
                bgQuests.GetComponent<Image>().DOFade(200f / 255f, 0.5f).SetUpdate(true).ToUniTask()
            );
        }

        public async void QuestsPanelOut()
        {
            await UniTask.WhenAll(
                panelQuests.transform.DOLocalMoveX(-1500f, 0.5f).SetUpdate(true).ToUniTask(),
                bgQuests.GetComponent<Image>().DOFade(0f, 0.5f).SetUpdate(true).ToUniTask()
            );
            bgQuests.SetActive(false);
        }

        public async void BallsPanelIn()
        {
            bgBalls.SetActive(true);
            await UniTask.WhenAll(
                panelBalls.transform.DOLocalMoveX(0f, 0.5f).SetUpdate(true).ToUniTask(),
                bgBalls.GetComponent<Image>().DOFade(200f / 255f, 0.5f).SetUpdate(true).ToUniTask()
            );
        }
        
        public async void BallsPanelOut()
        {
            await UniTask.WhenAll(
                panelBalls.transform.DOLocalMoveX(-1500f, 0.5f).SetUpdate(true).ToUniTask(),
                bgBalls.GetComponent<Image>().DOFade(0f, 0.5f).SetUpdate(true).ToUniTask()
            );
            bgBalls.SetActive(false);
        }
    }
}
