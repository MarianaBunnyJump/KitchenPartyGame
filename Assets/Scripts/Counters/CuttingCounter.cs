using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class CuttingCounter : BaseCounter, IHasProgress
    {
        [SerializeField] private CuttingRecipeSO[] cuttingRecipeSoArray;
        private int cuttingProgress;

        public event EventHandler OnCut;
        public static event EventHandler OnAnyCut;
        public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;


        public override void Interact(Player player)
        {
            if (!HasKitchenObject())
            {
                if (player.HasKitchenObject())
                {
                    if (HasRecipeInput(player.GetKitchenObject().GetKitchenObjectSO()))
                    {
                        player.GetKitchenObject().SetKitchenObjectParent(this);
                        cuttingProgress = 0;

                        CuttingRecipeSO cuttingRecipeSo =
                            GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = (float)cuttingProgress / cuttingRecipeSo.cuttingProgressMax
                        });
                    }
                }
                else
                {
                }
            }
            else
            {
                if (player.HasKitchenObject())
                {
                    if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                    {
                        if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                        {
                            GetKitchenObject().DestroySelf();
                        }
                    }
                }
                else
                {
                    GetKitchenObject().SetKitchenObjectParent(player);
                }
            }
        }

        //次要交互，切菜
        public override void InteractAlternate(Player player)
        {
            if (HasKitchenObject() && HasRecipeInput(GetKitchenObject().GetKitchenObjectSO()))
            {
                cuttingProgress++;
                OnCut?.Invoke(this, EventArgs.Empty);
                OnAnyCut?.Invoke(this, EventArgs.Empty);
                CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                });
                if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
                {
                    KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
                }
            }
        }

        private bool HasRecipeInput(KitchenObjectSO inputKitchenObjectSO)
        {
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
            return (cuttingRecipeSO != null);
        }

        private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
        {
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
            if (cuttingRecipeSO != null)
            {
                return cuttingRecipeSO.output;
            }

            return null;
        }

        private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
        {
            foreach (var cuttingRecipeSO in cuttingRecipeSoArray)
            {
                if (cuttingRecipeSO.input == inputKitchenObjectSO)
                {
                    return cuttingRecipeSO;
                }
            }

            return null;
        }
    }
}