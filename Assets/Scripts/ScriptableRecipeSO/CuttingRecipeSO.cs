using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "New CuttingRecipeSO", menuName = "CuttingRecipeSO")]
    public class CuttingRecipeSO : ScriptableObject
    {
        public KitchenObjectSO input;
        public KitchenObjectSO output;
        public int cuttingProgressMax;

    }
}