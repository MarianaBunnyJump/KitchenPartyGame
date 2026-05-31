using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlateKitchenObject : KitchenObject
    {
        [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;
        private List<KitchenObjectSO> kitchenObjectSOList;

        public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;

        public class OnIngredientAddedEventArgs : EventArgs
        {
            public KitchenObjectSO kitchenObjectSo;
        }

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
                OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs()
                {
                    kitchenObjectSo = kitchenObjectSO
                });
                return true;
            }
        }

        public List<KitchenObjectSO> GetKitchenObjectSo()
        {
            return kitchenObjectSOList;
        }
    }
}