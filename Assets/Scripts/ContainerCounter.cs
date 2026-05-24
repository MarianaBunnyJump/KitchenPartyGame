using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class ContainerCounter : BaseCounter
    {
        [SerializeField] private KitchenObjectSO kitchenObjectSo;
        public event EventHandler OnPlayerGrabbedObject;
        
        
        public override void Interact(Player player)
        {
            var kitchenObjectTransform = Instantiate(kitchenObjectSo.prefab);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
            OnPlayerGrabbedObject?.Invoke(this,EventArgs.Empty);
        }
    }
}