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
                EventManager.EmitEventData(EventNames.fireBarrierCollided, other.gameObject.transform.position);
                gameObject.SetActive(false);
            }
        }
    }
}
