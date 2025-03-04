using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BasketSpawner : MonoBehaviour
{
    public float boundsXMin, boundsXMax, boundsYMin, boundsYMax;
    public GameObject basketPrefab;
    public static List<GameObject> spawnedBaskets;
    public float minDistance;

    private void Start()
    {
        spawnedBaskets = new List<GameObject>();
    }

    public void SpawnPot(int basketCount, bool moving, bool disappear)
    {
        for (int i = 0; i < basketCount; i++)
        {
            Vector3 spawnPos;
            int attempts = 0;

            do
            {
                spawnPos = GetRandomPosition();
                attempts++;

                if (attempts > 10)
                {
                    Debug.LogWarning("Not found proper position!");
                    return;
                }

            } while (IsOverlapping(spawnPos));

            var basket = Instantiate(basketPrefab, spawnPos, Quaternion.Euler(new Vector3(-75f, 0, 0)));
            spawnedBaskets.Add(basket);
            if (moving)
            {
                basket.AddComponent<MovingBasket>();
            }

            if (disappear)
            {
                basket.AddComponent<DisappearingBasket>();
            }
        }
    }
    
    bool IsOverlapping(Vector3 newPosition)
    {
        foreach (var existingPos in spawnedBaskets)
        {
            if (Math.Abs(existingPos.transform.position.y - newPosition.y) < minDistance)
            {
                return true;
            }
        }
        return false;
    }

    private Vector3 GetRandomPosition()
    {
        var x = Random.Range(boundsXMin, boundsXMax);
        var y = Random.Range(boundsYMin, boundsYMax);
        return new Vector3(x, y, 0);
    }
}
