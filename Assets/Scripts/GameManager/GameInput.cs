using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    public class GameInput : MonoBehaviour
    {
        private const string PLAYER_PREF_BINDINGS = "InputBindings";
        public static GameInput Instance { get; private set; }

        public event EventHandler OnInteractAction;
        public event EventHandler OnInteractAlternateAction;
        public event EventHandler OnPauseAction;

        public enum Binding
        {
            Move_Up,
            Move_Down,
            Move_Left,
            Move_Right,
            Interact,
            InterAlternate,
            Pause,
            Gamepad_Interact,
            Gamepad_InteractAlternate,
            Gamepad_Pause
        }

        public event EventHandler OnResumeAction;
        private PlayerInputActions playerInputActions;

        private void Awake()
        {
            Instance = this;
            playerInputActions = new PlayerInputActions();
            if (PlayerPrefs.HasKey(PLAYER_PREF_BINDINGS))
            {
                playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREF_BINDINGS));
            }
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

        public string GetBindingText(Binding binding)
        {
            switch (binding)
            {
                default:
                case Binding.Move_Up:
                    return playerInputActions.Player.Move.bindings[1].ToDisplayString();
                    break;
                case Binding.Move_Down:
                    return playerInputActions.Player.Move.bindings[2].ToDisplayString();
                    break;
                case Binding.Move_Left:
                    return playerInputActions.Player.Move.bindings[3].ToDisplayString();
                    break;
                case Binding.Move_Right:
                    return playerInputActions.Player.Move.bindings[4].ToDisplayString();
                    break;

                case Binding.Interact:
                    return playerInputActions.Player.Interact.bindings[0].ToDisplayString();
                    break;
                case Binding.InterAlternate:
                    return playerInputActions.Player.InterAlternate.bindings[0].ToDisplayString();
                    break;
                case Binding.Pause:
                    return playerInputActions.Player.Pause.bindings[0].ToDisplayString();
                    break;
                case Binding.Gamepad_Interact:
                    return playerInputActions.Player.Interact.bindings[1].ToDisplayString();
                    break;
                case Binding.Gamepad_InteractAlternate:
                    return playerInputActions.Player.InterAlternate.bindings[1].ToDisplayString();
                    break;
                case Binding.Gamepad_Pause:
                    return playerInputActions.Player.Pause.bindings[1].ToDisplayString();
                    break;
            }
        }

        public void RebindBinding(Binding binding, Action onActionRebound)
        {
            playerInputActions.Player.Disable();

            InputAction inputAction;
            int bindingIndex;

            switch (binding)
            {
                default:
                case Binding.Move_Up:
                    inputAction = playerInputActions.Player.Move;
                    bindingIndex = 1;
                    break;
                case Binding.Move_Down:
                    inputAction = playerInputActions.Player.Move;
                    bindingIndex = 2;
                    break;
                case Binding.Move_Left:
                    inputAction = playerInputActions.Player.Move;
                    bindingIndex = 3;
                    break;
                case Binding.Move_Right:
                    inputAction = playerInputActions.Player.Move;
                    bindingIndex = 4;
                    break;
                case Binding.Interact:
                    inputAction = playerInputActions.Player.Interact;
                    bindingIndex = 0;
                    break;
                case Binding.InterAlternate:
                    inputAction = playerInputActions.Player.InterAlternate;
                    bindingIndex = 0;
                    break;
                case Binding.Pause:
                    inputAction = playerInputActions.Player.Pause;
                    bindingIndex = 0;
                    break;
                case Binding.Gamepad_Interact:
                    inputAction = playerInputActions.Player.Interact;
                    bindingIndex = 1;
                    break;
                case Binding.Gamepad_InteractAlternate:
                    inputAction = playerInputActions.Player.InterAlternate;
                    bindingIndex = 1;
                    break;
                case Binding.Gamepad_Pause:
                    inputAction = playerInputActions.Player.Pause;
                    bindingIndex = 1;
                    break;
                
            }

            inputAction.PerformInteractiveRebinding(bindingIndex).OnComplete(callback =>
            {
                callback.Dispose();
                playerInputActions.Player.Enable();
                onActionRebound();

                PlayerPrefs.SetString(PLAYER_PREF_BINDINGS,playerInputActions.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();
            }).Start();
        }
    }
}