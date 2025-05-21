using TigerForge;
using UnityEngine;

namespace Gameplay
{
    public class FireBarrierCollider : MonoBehaviour
    {
        void OnParticleCollision(GameObject other)
        {
            if (other.CompareTag("Ball"))
            {
                var ballRb = other.gameObject.GetComponent<Rigidbody>();
                if (ballRb != null)
                {
                    Vector2 hitDirection = (other.transform.position - transform.position).normalized;

                    ballRb.AddForce(hitDirection * 40f, ForceMode.Impulse);
                }
            }
        }
    }
}
