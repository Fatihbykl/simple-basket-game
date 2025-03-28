using System;
using Gameplay;
using Managers;
using UnityEngine;

namespace PowerUp
{
    public class SkillContext
    {
        public Ball ball;
        public ScoreManager scoreManager;

        public SkillContext(Ball ball, ScoreManager scoreManager)
        {
            this.ball = ball;
            this.scoreManager = scoreManager;
        }
    }
    [Serializable]
    public class PowerUp
    {
        public int count;
        public int duration;
        public Sprite icon;
        public bool isActive;

        public virtual void Activate(SkillContext context)
        {
            if (isActive) { return; }
            count--;
        }
    }
}
