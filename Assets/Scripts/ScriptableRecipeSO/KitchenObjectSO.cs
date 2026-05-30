using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "New KitchenObjectSO", menuName = "KitchenObjectSO")]
    public class KitchenObjectSO : ScriptableObject
    {
        public Transform prefab;
        public Sprite sprite;
        public string ObjectNames;
    }
}