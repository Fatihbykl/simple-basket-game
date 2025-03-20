using System;
using System.Collections.Generic;
using UnityEngine;

public class DisappearDifficulty : Difficulty
{
    private GameObject _effect;
    public DisappearDifficulty(GameObject effect)
    {
        _effect = effect;
    }
    public override void ApplyDifficulty(List<GameObject> baskets)
    {
        foreach (var basket in baskets)
        {
            basket.AddComponent<DisappearingBasket>().StartDisappearingBasket(_effect);
        }
    }
}
