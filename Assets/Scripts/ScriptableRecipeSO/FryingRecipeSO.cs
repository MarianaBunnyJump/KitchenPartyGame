using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "New FryingObjectSO", menuName = "FryingObjectSO")]
    public class FryingRecipeSO : ScriptableObject
    {
        public KitchenObjectSO input;
        public KitchenObjectSO output;
        public float fryingTimerMax;
    }
}