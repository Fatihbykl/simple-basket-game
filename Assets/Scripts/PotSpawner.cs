using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PotSpawner : MonoBehaviour
{
    public float boundsXMin, boundsXMax, boundsYMin, boundsYMax;
    public GameObject potPrefab;
    public static List<GameObject> spawnedPots;

    private void Start()
    {
        spawnedPots = new List<GameObject>();
    }

    public void SpawnPot()
    {
        var pot = Instantiate(potPrefab, GetRandomPosition(), Quaternion.Euler(new Vector3(-75f, 0, 0)));
        spawnedPots.Add(pot);
    }

    private Vector3 GetRandomPosition()
    {
        var x = Random.Range(boundsXMin, boundsXMax);
        var y = Random.Range(boundsYMin, boundsYMax);
        return new Vector3(x, y, 0);
    }
}
