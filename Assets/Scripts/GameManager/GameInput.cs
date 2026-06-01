using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameInput : MonoBehaviour
    {
        public static GameInput Instance{get; private set;}
        
        public event EventHandler OnInteractAction;
        public event EventHandler OnInteractAlternateAction;
        public event EventHandler OnPauseAction;
        public event EventHandler OnResumeAction;
        private PlayerInputActions playerInputActions;

        private void Awake()
        {
            Instance = this;
            playerInputActions = new PlayerInputActions();
            playerInputActions.Player.Enable();
            playerInputActions.Player.Interact.performed += Interact_performed;
            playerInputActions.Player.InterAlternate.performed += InterAlternate_performed;
            playerInputActions.Player.Pause.performed += Pause_performed;
        }

        private void OnDestroy()
        {
            playerInputActions.Player.Interact.performed -= Interact_performed;
            playerInputActions.Player.InterAlternate.performed -= InterAlternate_performed;
            playerInputActions.Player.Pause.performed -= Pause_performed;
            
            playerInputActions.Dispose();
        }

        private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            OnPauseAction?.Invoke(this, EventArgs.Empty);
        }

        private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            OnInteractAction?.Invoke(this, EventArgs.Empty);
        }
        
        private void InterAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
        }

        public Vector2 GetMovementVectorNormalized()
        {
            Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
            /*if (Input.GetKey(KeyCode.W))
            {
                inputVector.y = +1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                inputVector.y = -1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                inputVector.x = -1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                inputVector.x = +1;
            }*/

            inputVector = inputVector.normalized;
            return inputVector;
        }
    }
}