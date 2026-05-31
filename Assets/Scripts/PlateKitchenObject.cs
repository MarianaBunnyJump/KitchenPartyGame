using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlateKitchenObject : KitchenObject
    {
        [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;
        private List<KitchenObjectSO> kitchenObjectSOList;

        private void Awake()
        {
            kitchenObjectSOList = new List<KitchenObjectSO>();
        }

        public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
        {
            if (!validKitchenObjectSOList.Contains(kitchenObjectSO))
            {
                return false;
            }
            if (kitchenObjectSOList.Contains(kitchenObjectSO))
            {
                return false;
            }
            else
            {
                kitchenObjectSOList.Add(kitchenObjectSO);
                return true;
            }
        }
    }
}