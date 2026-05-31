using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace DefaultNamespace
{
   // [CreateAssetMenu()] 只需要一个，用完注了
    public class RecipeListSO : ScriptableObject
    {
        public List<RecipeSO> recipeSOList;
    }
}