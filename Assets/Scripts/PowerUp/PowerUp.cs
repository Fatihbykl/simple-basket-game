using System;
using System.Threading;
using Cysharp.Threading.Tasks;
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
        public int remainingTime;
        private CancellationTokenSource cts;
        
        public virtual void Activate(SkillContext context) {}
        
        public void StartTimer(int seconds, Action onComplete)
        {
            cts?.Cancel();
            cts = new CancellationTokenSource();
            TimerRoutine(seconds, onComplete, cts.Token).Forget();
        }

        private async UniTaskVoid TimerRoutine(int seconds, Action onComplete, CancellationToken token)
        {
            for (int i = seconds; i >= 0; i--)
            {
                if (token.IsCancellationRequested) return;

                remainingTime = i;
                await UniTask.Delay(1000, cancellationToken: token);
            }

            onComplete?.Invoke();
        }

        private void OnDestroy()
        {
            cts?.Cancel();
        }
    }
}
