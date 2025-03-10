using System;
using System.Collections.Generic;
using UnityEngine;

public class MovingDifficulty : Difficulty
{
    public override void ApplyDifficulty(List<GameObject> baskets)
    {
        foreach (var basket in baskets)
        {
            basket.AddComponent<MovingBasket>();
        }
    }
}
