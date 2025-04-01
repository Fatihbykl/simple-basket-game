using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace PowerUp
{
    [Serializable]
    public class TinyBallPowerUp : PowerUp
    {
        public override void Activate(SkillContext context)
        {
            if (isActive || count == 0) { return; }
            count--;
            
            ActivatePowerUp(context);
            StartTimer(30, () => DeactivatePowerUp(context));
        }

        private void ActivatePowerUp(SkillContext context)
        {
            isActive = true;
            context.ball.gameObject.transform.DOScale(new Vector3(31f / 2, 31f / 2, 31f / 2), 0.5f);
        }
        
        private void DeactivatePowerUp(SkillContext context)
        {
            isActive = false;
            context.ball.gameObject.transform.DOScale(new Vector3(31f, 31f, 31f), 0.5f);
        }
    }
}
