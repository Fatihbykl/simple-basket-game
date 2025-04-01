using System;
using Cysharp.Threading.Tasks;
using Gameplay;
using Managers;
using UnityEngine;

namespace PowerUp
{
    [Serializable]
    public class DoubleScorePowerUp : PowerUp
    {
        public override void Activate(SkillContext context)
        {
            if (isActive || count == 0) { return; }
            count--;
            
            ActivateScore(context.scoreManager, context.ball);
            StartTimer(30, () => DeactivateScore(context.scoreManager, context.ball));
        }

        private void ActivateScore(ScoreManager manager, Ball ball)
        {
            isActive = true;
            ball.doubleScoreText.SetActive(true);
            manager.scoreMultiplier = 2;
        }

        private void DeactivateScore(ScoreManager manager, Ball ball)
        {
            ball.doubleScoreText.SetActive(false);
            manager.scoreMultiplier = 1;
            isActive = false;
        }
    }
}
