using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI recipeDeliveredText;

        private void Start()
        {
            KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
            Hide();
        }

        private void KitchenGameManager_OnStateChanged(object sender, System.EventArgs e)
        {
            if (KitchenGameManager.Instance.IsGameOver())
            {
                Show();
                recipeDeliveredText.text = DeliveryManager.Instance.GetSuccessfulRecipesAmount().ToString();
            }
            else
            {
                Hide();
            }
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}