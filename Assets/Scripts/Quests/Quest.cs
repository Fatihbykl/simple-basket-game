using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Quests
{
    public enum TargetData
    {
        TotalScore,
        BestScore,
        BestCombo,
        GameCount
    }

    public enum RewardType
    {
        Default,
        TrailBlue1,
        TrailOrange2,
        TrailOrange3,
        TrailPurple4,
        TrailGreen5,
        OrbOrange6,
        ElectricBall7,
        FireballGreen8,
        FireballOrange9,
        FireballPurple10
    }
    
    [System.Serializable]
    public class Quest
    {
        public RewardType rewardType;
        [JsonIgnore] public string rewardDisplayName;
        [JsonIgnore] public string description;
        [JsonIgnore] public int goal;
        public int progress;
        public bool isCompleted;
        [JsonIgnore] public Sprite rewardIcon;
        [JsonIgnore] public TargetData target;
    }
}