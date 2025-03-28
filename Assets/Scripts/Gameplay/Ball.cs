using Managers;
using Quests;
using TigerForge;
using UnityEngine;

namespace Gameplay
{
    public class Ball : MonoBehaviour
    {
        public ParticleSystem bombCollideEffect;
        public ParticleSystem rocketCollideEffect;
        public ParticleSystem fireBarrierCollideEffect;
        public ParticleSystem shieldEffect;
        [HideInInspector] public bool isShieldActive;
        private Rigidbody _rb;

        private void Start()
        {
            EventManager.StartListening(EventNames.BombExploded, OnBombExploded);
            EventManager.StartListening(EventNames.RocketCollided, OnRocketCollided);
            EventManager.StartListening(EventNames.fireBarrierCollided, OnFireBarrierCollided);

            _rb = GetComponent<Rigidbody>();
            ActivateEffect((int)GameDataManager.instance.data.selectedBall);
        }

        public void ActivateShield()
        {
            isShieldActive = true;
            shieldEffect.Play();
        }

        public void DeactivateShield()
        {
            isShieldActive = false;
            shieldEffect.Stop();
        }

        private void ActivateEffect(int index)
        {
            SetMaterialTransparent();
            ResetEffects();
            transform.GetChild(index).gameObject.SetActive(true);
        }

        private void SetMaterialTransparent()
        {
            var alpha = 1f;
            
            if (GameDataManager.instance.data.selectedBall != RewardType.Default)
            {
                alpha = 200 / 255f;
            }
            
            foreach (var material in GetComponent<MeshRenderer>().materials)
            {
                Color color = material.color;
                color.a = alpha;
                material.color = color;
            }
        }

        private void ResetEffects()
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }

        public void ResetBallPosition()
        {
            transform.gameObject.transform.position = new Vector3(0, 4f, 0);
            _rb.linearVelocity = Vector3.zero;
        }

        private void OnBombExploded()
        {
            var pos = (Vector3)EventManager.GetData(EventNames.BombExploded);
            bombCollideEffect.transform.position = pos;
            bombCollideEffect.gameObject.SetActive(true);
        }

        private void OnRocketCollided()
        {
            var pos = (Vector3)EventManager.GetData(EventNames.RocketCollided);
            rocketCollideEffect.transform.position = pos;
            rocketCollideEffect.gameObject.SetActive(true);
        }

        private void OnFireBarrierCollided()
        {
            var pos = (Vector3)EventManager.GetData(EventNames.fireBarrierCollided);
            fireBarrierCollideEffect.transform.position = pos;
            fireBarrierCollideEffect.gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            EventManager.StopListening(EventNames.BombExploded, OnBombExploded);
            EventManager.StopListening(EventNames.RocketCollided, OnRocketCollided);
            EventManager.StopListening(EventNames.fireBarrierCollided, OnFireBarrierCollided);
        }
    }
}
