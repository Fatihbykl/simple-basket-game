using DG.Tweening;
using TigerForge;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class BasketCollider : MonoBehaviour
    {
        public GameObject bomb;
        public GameObject particlePrefab;
        public float rotationSpeed;
        private bool _isCollided;
        private Vector3 _normalScale = new Vector3(0.55f, 0.55f, 0.5f);
        private Vector2 _entryPosition;
        private Vector2 _entryVelocity;
        private bool _hasEntered = false;
        private float _perfectThreshold = 0.15f;

        private void Start()
        {
            transform.parent.localScale = Vector3.zero;
            transform.parent.DOScale(_normalScale, 0.5f);
        }

        private void Update()
        {
            transform.parent.Rotate(0f, 0f, rotationSpeed *  Time.deltaTime);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ball"))
            {
                var ballRb = other.GetComponent<Rigidbody>();

                _entryPosition = other.transform.position;
                _entryVelocity = ballRb.linearVelocity;
                _hasEntered = true;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Ball") && _hasEntered)
            {
                Rigidbody ballRb = other.GetComponent<Rigidbody>();

                Vector2 exitPosition = other.transform.position;
                Vector2 exitVelocity = ballRb.linearVelocity;

                if (IsValidBasket(_entryPosition, _entryVelocity, exitPosition, exitVelocity))
                {
                    bool isPerfect = IsPerfectBasket(_entryVelocity, exitVelocity);
                    GetComponent<BoxCollider>().enabled = false;
                    EventManager.SetData(EventNames.Score, isPerfect);
                    EventManager.EmitEvent(EventNames.Score, gameObject);
                    EventManager.EmitEvent(EventNames.Combo, gameObject);
                    Die();
                    EventManager.EmitEvent(EventNames.Basket);
                    CreateFloatingText(isPerfect);
                }
                _hasEntered = false;
            }
        }

        private void CreateFloatingText(bool isPerfect)
        {
            var destination = transform.position;
            destination.x += (Random.value - 0.5f) / 3f;
            destination.y += Random.value;
            destination.z += (Random.value - 0.5f) / 3f;

            if (isPerfect)
            {
                DynamicTextManager.CreateText(destination, "PERFECT!", DynamicTextManager.perfectBasketData);
            }
        }

        private bool IsValidBasket(Vector2 entryPos, Vector2 entryVel, Vector2 exitPos, Vector2 exitVel)
        {
            float hoopY = transform.position.y;
            bool enteredFromTop = entryPos.y > hoopY;
            bool enteredFromBottom = entryPos.y < hoopY;
            bool exitedFromTop = exitPos.y > hoopY;
            bool exitedFromBottom = exitPos.y < hoopY;

            if ((enteredFromTop && exitedFromBottom) || (enteredFromBottom && exitedFromTop))
                return true;

            return false;
        }

        private bool IsPerfectBasket(Vector2 entryVel, Vector2 exitVel)
        {
            var hoopUp = -transform.forward;
            float entryDot = Mathf.Abs(Vector2.Dot(entryVel.normalized, hoopUp));
            float exitDot = Mathf.Abs(Vector2.Dot(exitVel.normalized, hoopUp));

            return entryDot > (1 - _perfectThreshold) && exitDot > (1 - _perfectThreshold);
        }

        public void Die()
        {
            var particle = Instantiate(particlePrefab, transform.position, Quaternion.identity);
            BasketSpawner.spawnedBaskets.Remove(transform.parent.gameObject);
            GetComponentInParent<MeshCollider>().enabled = false;
            transform.parent.DOScale(Vector3.zero, 0.5f);
            Destroy(transform.parent.gameObject, 1f);
            Destroy(particle, 1f);

            if (bomb)
            {
                bomb.GetComponent<Animator>().SetTrigger("Die");
                Destroy(bomb, 1f);
            }
        }
    }
}
