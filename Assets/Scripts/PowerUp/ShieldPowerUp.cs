using System;
using Cysharp.Threading.Tasks;
using Gameplay;
using UnityEngine;

namespace PowerUp
{
    [Serializable]
    public class ShieldPowerUp : PowerUp
    {
        public override void Activate(SkillContext context)
        {
            if (isActive || count == 0) { return; }
            count--;
            
            ActivateShield(context.ball);
            StartTimer(30, () => DeactivateShield(context.ball));
        }

        private void ActivateShield(Ball ball)
        {
            isActive = true;
            ball.ActivateShield();
        }
        
        private void DeactivateShield(Ball ball)
        {
            isActive = false;
            ball.DeactivateShield();
        }
    }
}
