using System.Collections.Generic;
using Gameplay;
using UnityEngine;

public class BombDifficulty : Difficulty
{
    private const float Offset = 1.25f;
    private GameObject _prefab;

    public BombDifficulty(GameObject bombPrefab)
    {
        _prefab = bombPrefab;
    }

    public override void ApplyDifficulty(List<GameObject> baskets)
    {
        var basket = baskets[0];
        var basketPosition = basket.transform.position;

        var leftLimit = CameraBounds.leftWallPos.x;
        var rightLimit = CameraBounds.rightWallPos.x;
        
        var bombPosition = new Vector3(basketPosition.x + Offset, basketPosition.y, basketPosition.z);

        if (bombPosition.x > rightLimit)
        {
            bombPosition.x = basketPosition.x - Offset;
        }

        if (bombPosition.x < leftLimit)
        {
            Debug.Log("Bomb couldn't placed!");
            return;
        }

        var bomb = Object.Instantiate(_prefab, bombPosition, _prefab.transform.rotation);
        basket.GetComponentInChildren<BasketCollider>().bomb = bomb;
    }
}
