using UnityEngine;

namespace Quests
{
    [System.Serializable]
    public class Quest
    {
        public string questName;
        public string description;
        public int goal;
        public int progress;
        public bool isCompleted;
        public string reward;
    }
}