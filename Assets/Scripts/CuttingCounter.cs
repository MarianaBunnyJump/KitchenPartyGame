using UnityEngine;

namespace DefaultNamespace
{
    public class CuttingCounter : BaseCounter
    {
        [SerializeField] private KitchenObjectSO cutKitchenObjectSo;
        public override void Interact(Player player)
        {
            if(!HasKitchenObject())
            {
                if (player.HasKitchenObject())
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
                else
                {
                    
                }
            }
            else
            {
                if (player.HasKitchenObject())
                {
                    
                }
                else
                {
                    GetKitchenObject().SetKitchenObjectParent(player);
                }
            }
        }
        
        //次要交互，切菜
        public override void InteractAlternate(Player player)
        {
            if (HasKitchenObject())
            {
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(cutKitchenObjectSo, this);

            }
        }
        
        
    }
}


















