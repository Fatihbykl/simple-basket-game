using System;
using Cysharp.Threading.Tasks;
using Managers;
using UnityEngine;

namespace PowerUp
{
    [Serializable]
    public class DoubleScorePowerUp : PowerUp
    {
        public override void Activate(SkillContext context)
        {
            base.Activate(context);
            ActivateScore(context.scoreManager);
        }

        private async void ActivateScore(ScoreManager manager)
        {
            isActive = true;
            manager.scoreMultiplier = 2;
            
            await UniTask.WaitForSeconds(duration);
            
            manager.scoreMultiplier = 1;
            isActive = false;
        }
    }
}
