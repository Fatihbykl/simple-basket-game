using TigerForge;
using UnityEngine;

public class FireBarrierCollider : MonoBehaviour
{
    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Ball"))
        {
            EventManager.EmitEventData(EventNames.fireBarrierCollided, other.gameObject.transform.position);
            EventManager.EmitEvent(EventNames.fireBarrierCollided);
            gameObject.SetActive(false);
        }
    }
}
