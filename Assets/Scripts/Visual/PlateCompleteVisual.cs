using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlateCompleteVisual : MonoBehaviour
    {
        [SerializeField] private PlateKitchenObject plateKitchenObject;
        [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSoGameObjects;

        [Serializable]
        private struct KitchenObjectSO_GameObject
        {
            public KitchenObjectSO kitchenObjectSO;
            public GameObject gameObject;
        }

        private void Start()
        {
            plateKitchenObject.OnIngredientAdded += plateKitchenObject_OnIngredientAdded;
            foreach (var kitchenObjectSoGameObject in kitchenObjectSoGameObjects)
            {
                kitchenObjectSoGameObject.gameObject.SetActive(false);
            }
        }

        private void plateKitchenObject_OnIngredientAdded(object sender,
            PlateKitchenObject.OnIngredientAddedEventArgs e)
        {
            
            foreach (var objectSoGameObject in kitchenObjectSoGameObjects)
            {
                if (objectSoGameObject.kitchenObjectSO == e.kitchenObjectSo)
                {
                    objectSoGameObject.gameObject.SetActive(true);
                }
            }
            
        }
    }
}