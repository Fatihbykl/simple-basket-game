using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Difficulties
{
    public class Rotations
    {
        public float X;
        public float Y;

        public Rotations(float x, float y)
        {
            X = x;
            Y = y;
        }
        
        public Quaternion GetRotation()
        {
            return Quaternion.Euler(X, Y, 0);
        }
    }
    
    public class RotationDifficulty : Difficulty
    {
        private Rotations[] _rotationsArray = new[]
        {
            new Rotations(-75f, 0),
            new Rotations(-65f, 75),
            new Rotations(65f, 75),
            new Rotations(75f, 75),
            new Rotations(-75f, 75),
            new Rotations(85f, 75),
            new Rotations(-85f, 75)
        };

        public override void ApplyDifficulty(List<GameObject> baskets)
        {
            foreach (var basket in baskets)
            {
                var rotation = _rotationsArray[Random.Range(0, _rotationsArray.Length - 1)];
                basket.transform.rotation = rotation.GetRotation();
            }
        }
    }
}
