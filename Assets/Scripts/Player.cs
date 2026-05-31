using System;
using DefaultNamespace;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    //static属于player类
    private static Player instance;
    public static Player Instance { get; private set; }

    public event EventHandler OnPickedSomething;

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounterOne;
    }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;
    private KitchenObject kitchenObject;

    private bool isWalking;
    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Instance is null");
        }

        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInterAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInterAction(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleMovement()
    {
        //得到带方向的移动向量
        var inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        isWalking = moveDir != Vector3.zero;

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
            playerRadius, moveDir, moveDistance);

        //撞墙仍然可以分解向量向横向或者纵向移动，忽略撞的方向
        if (!canMove)
        {
            //分解如果X方向可以移动
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            //如果 moveDir 是 (0,0,0)，Unity 的 Physics.CapsuleCast 会返回 false（因为零向量没有方向，无法进行有效的碰撞检测）所以 canMove = !false = true
            //这样就无法进行Z方向的纯旋转了
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position,
                transform.position + Vector3.up * playerHeight,
                playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                //如果X方向不可以移动，则尝试Z方向
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position,
                    transform.position + Vector3.up * playerHeight,
                    playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }

        //角色朝向移动方向
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, -moveDir, Time.deltaTime * rotateSpeed);
    }
    
    private void HandleInteractions()
    {
        var inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance,
                countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
                //如果已经是同一个柜台则什么都不做，保持选中
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounterOne = selectedCounter
        });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null)
        {
            OnPickedSomething?.Invoke(this,EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}