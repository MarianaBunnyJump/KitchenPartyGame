using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class PlateIconSingleUI : MonoBehaviour
    {
        [SerializeField] private Image image;
        
        public void SetKitchenObjectSO(KitchenObjectSO kitchenObjectSo)
        {
            image.sprite = kitchenObjectSo.sprite;
        }
    }
}