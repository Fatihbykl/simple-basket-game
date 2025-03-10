using System;
using System.Collections.Generic;
using UnityEngine;

public class DisappearDifficulty : Difficulty
{
    public override void ApplyDifficulty(List<GameObject> baskets)
    {
        foreach (var basket in baskets)
        {
            basket.AddComponent<DisappearingBasket>();
        }
    }
}
