using System;
using DG.Tweening;
using TigerForge;
using UnityEngine;

public class BasketCollider : MonoBehaviour
{
    public GameObject bomb;
    public float rotationSpeed;
    private bool _isCollided;
    private Vector3 _normalScale = new Vector3(0.55f, 0.55f, 0.5f);

    private void Start()
    {
        transform.parent.localScale = Vector3.zero;
        transform.parent.DOScale(_normalScale, 0.5f);
    }

    private void Update()
    {
        transform.parent.Rotate(0f, 0f, rotationSpeed *  Time.deltaTime);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Ball")) { return; }
        if (_isCollided) { return; }

        BasketSpawner.spawnedBaskets.Remove(transform.parent.gameObject);
        EventManager.EmitEvent(EventNames.Basket);
        _isCollided = true;
        Die();
    }

    public void Die()
    {
        transform.parent.DOScale(Vector3.zero, 0.5f);
        Destroy(transform.parent.gameObject, 1f);

        if (bomb)
        {
            bomb.GetComponent<Animator>().SetTrigger("Die");
            Destroy(bomb, 1f);
        }
    }
}
