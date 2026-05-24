using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class ClearCounter : BaseCounter
    {
        [SerializeField] private KitchenObjectSO kitchenObjectSo;

        public override void Interact(Player player)
        {
        }
    }
}