using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Difficulties
{
    public class WheelDifficulty : Difficulty
    {
        private GameObject _wheelPrefab;
        private float _offset = 1.5f;
        private List<GameObject> _currentBaskets;

        public WheelDifficulty(GameObject wheel)
        {
            _wheelPrefab = wheel;
        }
        
        public override void ApplyDifficulty(List<GameObject> baskets)
        {
            _currentBaskets = baskets;
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
            Vector3 basketPosition = _currentBaskets[0].transform.position;
            
            spawnPosition = new Vector3(basketPosition.x + _offset, basketPosition.y, basketPosition.z);

            if (spawnPosition.x > rightLimit)
            {
                spawnPosition.x = basketPosition.x - _offset;
            }

            if (spawnPosition.x < leftLimit)
            {
                Debug.Log("Wheel couldn't placed!");
                return;
            }
            
            GameObject.Instantiate(_wheelPrefab, spawnPosition, _wheelPrefab.transform.rotation);
        }
    }
}
