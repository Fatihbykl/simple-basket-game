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
            base.Activate(context);
            ActivateShield(context.ball);
        }

        private async void ActivateShield(Ball ball)
        {
            isActive = true;
            ball.ActivateShield();
            Debug.Log("Shield activated");
            
            await UniTask.WaitForSeconds(duration);
            Debug.Log("Shield deactivated");
            isActive = false;
            ball.DeactivateShield();
        }
    }
}
