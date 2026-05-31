using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu()]
    public class RecipeSO : ScriptableObject
    {
        public List<KitchenObjectSO> kitchenObjectSoList;
        public string recipeName;

    }
}