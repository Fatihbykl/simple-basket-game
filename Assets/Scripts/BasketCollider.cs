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
    private bool _enteredFromTop = false;
    private bool _enteredFromBottom = false;
    private bool _exitedFromTop = false;
    private bool _exitedFromBottom = false;
    private int _triggerCounter;
    private float _lastExit;

    private void Start()
    {
        transform.parent.localScale = Vector3.zero;
        transform.parent.DOScale(_normalScale, 0.5f);
    }

    private void Update()
    {
        transform.parent.Rotate(0f, 0f, rotationSpeed *  Time.deltaTime);
    }

    private void Scored()
    {
        if (_isCollided) { return; }

        BasketSpawner.spawnedBaskets.Remove(transform.parent.gameObject);
        EventManager.EmitEvent(EventNames.Basket);
        _isCollided = true;
        Die();
    }

    public void TriggerEnter(string side)
    {
        if (Time.time - _lastExit > 1f)
        {
            return;
        }
        switch (side)
        {
            case "Bottom":
                _enteredFromBottom = true;
                break;
            case "Top":
                _enteredFromTop = true;
                break;
        }

        _triggerCounter++;
        if(_triggerCounter == 3) { CheckBasket(); }
    }

    public void TriggerExit(string side)
    {
        if (_triggerCounter == 0) { return; }
        switch (side)
        {
            case "Bottom":
                _exitedFromBottom = true;
                break;
            case "Top":
                _exitedFromTop = true;
                break;
        }

        _lastExit = Time.time;
        _triggerCounter++;
        if(_triggerCounter == 3) { CheckBasket(); }
    }

    private void CheckBasket()
    {
        if (_enteredFromTop && _exitedFromTop && _enteredFromBottom)
        {
            Scored();
            Debug.Log("Basket! Üstten girip alttan çıktı.");
        }

        else if (_enteredFromBottom && _exitedFromBottom && _enteredFromTop)
        {
            Scored();
            Debug.Log("Basket! Alttan girip üstten çıktı.");
        }

        ResetTriggers();
    }

    private void ResetTriggers()
    {
        _enteredFromTop = false;
        _enteredFromBottom = false;
        _exitedFromTop = false;
        _exitedFromBottom = false;
        _triggerCounter = 0;
    }

    public void Die()
    {
        GetComponentInParent<MeshCollider>().enabled = false;
        transform.parent.DOScale(Vector3.zero, 0.5f);
        Destroy(transform.parent.gameObject, 1f);

        if (bomb)
        {
            bomb.GetComponent<Animator>().SetTrigger("Die");
            Destroy(bomb, 1f);
        }
    }
}
