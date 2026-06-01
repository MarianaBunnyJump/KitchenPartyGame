using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button startBtn;
        [SerializeField] private Button quitBtn;

        private void Awake()
        {
            startBtn.onClick.AddListener(PlayClick);
            quitBtn.onClick.AddListener(QuitClick);
            Time.timeScale = 1f;
        }

        private void PlayClick()
        {
            Loader.Load(Loader.Scene.GameScene);
        }

        private void QuitClick()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}