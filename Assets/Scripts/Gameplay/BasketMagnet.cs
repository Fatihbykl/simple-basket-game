using UnityEngine;

namespace Gameplay
{
    public class BasketMagnet : MonoBehaviour
    {
        public Transform hoopCenter;
        public float pullForce = 5f;

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Ball")) 
            {
                Rigidbody ballRb = other.GetComponent<Rigidbody>();
                Vector2 direction = (hoopCenter.position - other.transform.position).normalized;
                ballRb.AddForce(direction * pullForce);
            }
        }
    }
}
