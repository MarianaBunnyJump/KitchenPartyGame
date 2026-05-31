using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class ProgressBarUI : MonoBehaviour
    {
        [SerializeField] private GameObject hasProgressGameObject;
        [SerializeField] private Image barImage;
        private IHasProgress hasProgress;

        private void Start()
        {
            hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
            if (hasProgress == null)
            {
                Debug.LogError("hasProgress is null");
                return;
            }
            hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
            barImage.fillAmount = 0f;
            ShowOff(false);
        }

        private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
        {

            barImage.fillAmount = e.progressNormalized;
            if (e.progressNormalized == 0f || Mathf.Approximately(e.progressNormalized,1))
            {
                ShowOff(false);
            }
            else
            {
                ShowOff(true);
            }
        }

        private void ShowOff(bool isShow)
        {
            gameObject.SetActive(isShow);
        }
    }
}