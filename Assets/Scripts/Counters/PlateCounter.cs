using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;

namespace DefaultNamespace
{
    public class PlateCounter : BaseCounter
    {
        public event EventHandler OnPlateSpawned;
        public event EventHandler OnPlateRemoved;
        [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
        private float spawnPlateTimer;
        private float spawnPlateTimerMax = 4f;
        private int plateSpawnedAmount;
        private int plateSpawnedAmountMax = 4;

        private void Update()
        {
            spawnPlateTimer += Time.deltaTime;
            if (spawnPlateTimer > spawnPlateTimerMax)
            {
                spawnPlateTimer = 0f;
                if (plateSpawnedAmount < plateSpawnedAmountMax)
                {
                    plateSpawnedAmount++;
                    OnPlateSpawned?.Invoke(this,EventArgs.Empty);
                }
            }
        }

        public override void Interact(Player player)
        {
            if (!player.HasKitchenObject())
            {
                if (plateSpawnedAmount > 0)
                {
                    plateSpawnedAmount--;
                    KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                    OnPlateRemoved?.Invoke(this,EventArgs.Empty);
                }
            }
        }
        
        public override void InteractAlternate(Player player)
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}