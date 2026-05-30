using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "New BurningRecipeSO", menuName = "BurningRecipeSO")]
    public class BurningRecipeSO : ScriptableObject
    {
        public KitchenObjectSO input;
        public KitchenObjectSO output;
        public float burningTimerMax;
    }
}