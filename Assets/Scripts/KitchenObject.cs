using Unity.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class KitchenObject : MonoBehaviour
    {
        [SerializeField] private KitchenObjectSO kitchenObjectSO;
        private IKitchenObjectParent kitchenObjectParent;
        

        public KitchenObjectSO GetKitchenObjectSO()
        {
            return kitchenObjectSO;
        }

        public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
        {
            if (this.kitchenObjectParent != null)
            {
                this.kitchenObjectParent.ClearKitchenObject();
            }

            this.kitchenObjectParent = kitchenObjectParent;

            if (kitchenObjectParent.HasKitchenObject())
            {
                Debug.LogError("已经有物体了");
                return;
            }

            kitchenObjectParent.SetKitchenObject(this);

            transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
            transform.localPosition = Vector3.zero;
        }

        public IKitchenObjectParent GetClearCounter()
        {
            return kitchenObjectParent;
        }

        public void DestroySelf()
        {
            kitchenObjectParent.ClearKitchenObject();
            Destroy(gameObject);
        }

        public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
            KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
            kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
            return kitchenObject;
        }
    }
}