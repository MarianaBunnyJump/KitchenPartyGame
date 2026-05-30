using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class StoveCounter : BaseCounter,IHasProgress
    {
        public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
        public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
        

        public class OnStateChangedEventArgs : EventArgs
        {
            public State state;
        }
        
        public enum State
        {
            Idle,
            Frying,
            Fried,
            Burned,
        }

        [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
        [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

        private State state;
        private float fryingTimer;
        private float fryingBurnedTimer;
        private FryingRecipeSO fryingRecipeSo;
        private BurningRecipeSO burningRecipeSO;

        private void Start()
        {
            state = State.Idle;
        }

        private void Update()
        {
            if (HasKitchenObject())
            {
                switch (state)
                {
                    case State.Idle:
                        break;
                    case State.Frying:
                        fryingTimer += Time.deltaTime;
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = (float)fryingTimer / fryingRecipeSo.fryingTimerMax
                        });
                        if (fryingTimer > fryingRecipeSo.fryingTimerMax)
                        {
                            fryingTimer = 0f;
                            GetKitchenObject().DestroySelf();
                            KitchenObject.SpawnKitchenObject(fryingRecipeSo.output, this);
                            burningRecipeSO  = GetBuringRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                            state = State.Fried;
                            fryingBurnedTimer = 0f;
                        }
                        OnStateChanged?.Invoke(this,new OnStateChangedEventArgs
                        {
                            state = state
                        });
                      
                        break;
                    case State.Fried:
                        fryingBurnedTimer += Time.deltaTime;
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = (float)fryingBurnedTimer / burningRecipeSO.burningTimerMax
                        });
                        if (fryingBurnedTimer > burningRecipeSO.burningTimerMax)
                        {
                            GetKitchenObject().DestroySelf();
                            KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);
                            state = State.Burned;
                        }
                        
                        OnStateChanged?.Invoke(this,new OnStateChangedEventArgs
                        {
                            state = state
                        });

                        break;
                    case State.Burned:
                        break;
                }

            }
        }

        public override void Interact(Player player)
        {
            if (!HasKitchenObject())
            {
                if (player.HasKitchenObject())
                {
                    if (HasRecipeInput(player.GetKitchenObject().GetKitchenObjectSO()))
                    {
                        player.GetKitchenObject().SetKitchenObjectParent(this);
                        fryingRecipeSo =
                            GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                        state = State.Frying;
                        fryingTimer = 0f;
                        OnStateChanged?.Invoke(this,new OnStateChangedEventArgs
                        {
                            state = state
                        });
                        
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = (float)fryingTimer / fryingRecipeSo.fryingTimerMax
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
                }
                else
                {
                    Debug.Log("Object Idle");
                    GetKitchenObject().SetKitchenObjectParent(player);
                    state = State.Idle;
                    OnStateChanged?.Invoke(this,new OnStateChangedEventArgs
                    {
                        state = state
                    });
                    
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = 0f
                    });
                }
            }
        }

        public override void InteractAlternate(Player player)
        {
            if (HasKitchenObject() && HasRecipeInput(GetKitchenObject().GetKitchenObjectSO()))
            {
            }
        }

        private bool HasRecipeInput(KitchenObjectSO inputKitchenObjectSO)
        {
            FryingRecipeSO fryingRecipeSo = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
            return (fryingRecipeSo != null);
        }

        private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
        {
            FryingRecipeSO fryingRecipeSo = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
            if (fryingRecipeSo != null)
            {
                return fryingRecipeSo.output;
            }

            return null;
        }

        private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
        {
            foreach (var fryingRecipeSo in fryingRecipeSOArray)
            {
                if (fryingRecipeSo.input == inputKitchenObjectSO)
                {
                    return fryingRecipeSo;
                }
            }

            return null;
        }

        private BurningRecipeSO GetBuringRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
        {
            foreach (var burningRecipeSo in burningRecipeSOArray)
            {
                if (burningRecipeSo.input == inputKitchenObjectSO)
                {
                    return burningRecipeSo;
                }
            }

            return null;
        }

    }
}