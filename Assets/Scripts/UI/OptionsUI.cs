using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }
    [SerializeField] private Button soundEffectButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button backButton;

    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button InteractButton;
    [SerializeField] private Button InteractAltButton;
    [SerializeField] private Button PauseButton;
    
    [SerializeField] private Button Gamepad_Interact;
    [SerializeField] private Button Gamepad_InteractAlternate;
    [SerializeField] private Button Gamepad_Pause;
    

    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private Transform pressToRebindKey;

    private Action onCloseButtonAction;

    private void Awake()
    {
        Instance = this;
        soundEffectButton.onClick.AddListener((() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        }));

        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        backButton.onClick.AddListener(() =>
        {
            Hide();
            onCloseButtonAction.Invoke();
        });

        RegisterBtn();
    }

    private void Start()
    {
        KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGamePaused;
        UpdateVisual();

        HidePressToRebindKey();
        Hide();
    }

    private void KitchenGameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void RegisterBtn()
    {
        moveUpButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Up);
        });
        
        moveDownButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Down);
        });
        
        moveLeftButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Left);
        });
        
        moveRightButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Right);
        });
        
        InteractButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Interact);
        });
        
        InteractAltButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.InterAlternate);
        });
        
        PauseButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Pause);
        });
        
        Gamepad_Interact.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Gamepad_Interact);
        });
        
        Gamepad_InteractAlternate.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Gamepad_InteractAlternate);
        });
        
        Gamepad_Pause.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Gamepad_Pause);
        });
    }

    private void UpdateVisual()
    {
        soundEffectsText.text = "Sound Effects:" + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "Music:" + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);

        moveUpButton.GetComponentInChildren<TMP_Text>().text =
            GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);

        moveDownButton.GetComponentInChildren<TMP_Text>().text =
            GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);

        moveLeftButton.GetComponentInChildren<TMP_Text>().text =
            GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);

        moveRightButton.GetComponentInChildren<TMP_Text>().text =
            GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);

        InteractButton.GetComponentInChildren<TMP_Text>().text =
            GameInput.Instance.GetBindingText(GameInput.Binding.Interact);

        InteractAltButton.GetComponentInChildren<TMP_Text>().text =
            GameInput.Instance.GetBindingText(GameInput.Binding.InterAlternate);

        PauseButton.GetComponentInChildren<TMP_Text>().text =
            GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        
        Gamepad_Interact.GetComponentInChildren<TMP_Text>().text =
            GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
        
        Gamepad_InteractAlternate.GetComponentInChildren<TMP_Text>().text =
            GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_InteractAlternate);
        
        Gamepad_Pause.GetComponentInChildren<TMP_Text>().text =
            GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause);
    }

    public void Show(Action onCloseButtonAction)
    {
        this.onCloseButtonAction = onCloseButtonAction;
        gameObject.SetActive(true);
        soundEffectButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ShowPressToRebindKey()
    {
        pressToRebindKey.gameObject.SetActive(true);
    }
    
    private void HidePressToRebindKey()
    {
        pressToRebindKey.gameObject.SetActive(false);
    }

    private void RebindBinding(GameInput.Binding binding)
    {
        ShowPressToRebindKey();
        GameInput.Instance.RebindBinding(binding,() =>
        {
            HidePressToRebindKey();
            UpdateVisual();
        });
    }
}