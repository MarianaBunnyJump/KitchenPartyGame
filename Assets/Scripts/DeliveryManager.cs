using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class DeliveryManager : MonoBehaviour
    {
        public event EventHandler OnRecipeSpawned;
        public event EventHandler OnRecipeCompleted;
        public event EventHandler OnRecipeSuccess;
        public event EventHandler OnRecipeFailed;
        public static DeliveryManager Instance { get; private set; }
        
        [SerializeField] private RecipeListSO recipeListSO;
        private List<RecipeSO> waitingRecipeSOList;
        private float spawnRecipeTimer;
        private float spawnRecipeTimerMax = 4f;
        private int waitingRecipesMax = 4;

        private void Awake()
        {
            Instance = this;
            
            waitingRecipeSOList = new List<RecipeSO>();
            spawnRecipeTimer = spawnRecipeTimerMax;
        }

        private void Update()
        {
            
            spawnRecipeTimer -= Time.deltaTime;
            if (spawnRecipeTimer <= 0f)
            {
                spawnRecipeTimer = spawnRecipeTimerMax;

                if (waitingRecipeSOList.Count < waitingRecipesMax)
                {
                    RecipeSO waitingRecipeSO =
                        recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
                    //Debug.Log(waitingRecipeSO.recipeName);
                    waitingRecipeSOList.Add(waitingRecipeSO);
                    OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
        {
            for (int i = 0; i < waitingRecipeSOList.Count; i++)
            {
                RecipeSO waitingRecipeSO = waitingRecipeSOList[i];
                if (waitingRecipeSO.kitchenObjectSoList.Count == plateKitchenObject.GetKitchenObjectSoList().Count)
                {

                    bool plateContentsMatchesRecipe = true;
                    foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSoList)
                    {
                        bool ingredientFount = false;
                        foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSoList())
                        {
                            if (plateKitchenObjectSO == recipeKitchenObjectSO)
                            {
                                ingredientFount = true;
                                break;
                            }
                        }
                        if (!ingredientFount)
                        {
                            plateContentsMatchesRecipe = false;
                        }
                    }

                    if (plateContentsMatchesRecipe)
                    {
                        Debug.Log("player delivered the correct recipe");
                        waitingRecipeSOList.RemoveAt(i);
                        
                        OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                        OnRecipeSuccess?.Invoke(this,EventArgs.Empty);
                        return;
                    }
                }
            }
            Debug.Log("player no match recipe");
            OnRecipeFailed?.Invoke(this,EventArgs.Empty);
        }
        
        public List<RecipeSO> GetWaitingRecipeSOList()
        {
            return waitingRecipeSOList;
        }
    }
}














