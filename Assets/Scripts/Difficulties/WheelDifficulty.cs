using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Difficulties
{
    public class WheelDifficulty : Difficulty
    {
        private GameObject _wheelPrefab;
        private float _stepSize = 0.5f;
        private float _minDistanceFromBaskets = 1.5f;
        private List<GameObject> _currentBaskets;

        public WheelDifficulty(GameObject wheel)
        {
            _wheelPrefab = wheel;
        }
        
        public override void ApplyDifficulty(List<GameObject> baskets)
        {
            _currentBaskets = baskets;
            Debug.Log(baskets.Count);
            SpawnObstacle();
        }
        
        private void SpawnObstacle()
        {
            float leftLimit = CameraBounds.leftWallPos.x + 1f;
            float rightLimit = CameraBounds.rightWallPos.x - 1f;
            var upperLimit = CameraBounds.topWallPos.y - 1f;
            var lowerLimit = CameraBounds.bottomWallPos.y + 1f;

            float randomX = Random.Range(leftLimit, rightLimit);
            float randomY = Random.Range(lowerLimit, upperLimit);
            Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);

            spawnPosition.x = FindNearestValidPosition(spawnPosition.x, leftLimit, rightLimit);

            GameObject.Instantiate(_wheelPrefab, spawnPosition, _wheelPrefab.transform.rotation);
        }

        private float FindNearestValidPosition(float startX, float leftLimit, float rightLimit)
        {
            float x = startX;

            for (int i = 0; i < 10; i++)
            {
                if (!IsCollidingWithPots(x))
                    return x;

                x += _stepSize;
                if (x > rightLimit)
                {
                    x = startX - _stepSize;
                }
            }
            
            return Mathf.Clamp(startX, leftLimit, rightLimit);
        }

        private bool IsCollidingWithPots(float x)
        {
            foreach (var pota in _currentBaskets)
            {
                if (Mathf.Abs(x - pota.transform.position.x) < _minDistanceFromBaskets)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
