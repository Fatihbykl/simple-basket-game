using Cysharp.Threading.Tasks;
using Gameplay;
using TigerForge;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        public BasketSpawner spawner;
        [SerializeField] private int _currentLevel;

        private void Start()
        {
            EventManager.StartListening(EventNames.Basket, OnBasket);
            LoadLevel();
        }

        private void OnBasket()
        {
            LoadLevel();
        }

        private async UniTask LoadLevel()
        {
            if (BasketSpawner.spawnedBaskets.Count != 0) { return; }

            _currentLevel++;
            EventManager.EmitEventData(EventNames.LevelUpgraded, _currentLevel);
        
            int basketCount = 1;
        
            if (_currentLevel >= 40) basketCount = 2;
            if (_currentLevel >= 50) basketCount = 3;

            await spawner.SpawnPot(basketCount);
            EventManager.EmitEvent(EventNames.LevelLoaded);
        }

        private void OnDestroy()
        {
            EventManager.StopListening(EventNames.Basket, OnBasket);
        }
    }
}
