using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class QuestRow : MonoBehaviour
    {
        public Image rewardIcon;
        public TextMeshProUGUI rewardName;
        public TextMeshProUGUI description;
        public TextMeshProUGUI progressText;
        public Slider progressSlider;
        public Image sliderFill;
        public Sprite sliderFillGreen;

        public QuestRow(Sprite icon, string name, string desc, int progress)
        {
            rewardIcon.sprite = icon;
            rewardName.text = name;
            description.text = desc;
            progressText.text = progress.ToString();
            progressSlider.value = progress;
        }
    }
}
