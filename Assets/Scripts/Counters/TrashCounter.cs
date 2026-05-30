using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class TrashCounter : BaseCounter
    {
        public override void Interact(Player player)
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}