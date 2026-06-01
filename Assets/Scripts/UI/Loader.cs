using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public static class Loader
    {
        public enum Scene
        {
            MainMenuScene,
            GameScene,
            LoadingScene
        }

        private static Scene targetScene;

        public static void Load(Scene targetScene)
        {
            Loader.targetScene = targetScene;
            SceneManager.LoadScene(Scene.LoadingScene.ToString());
        }
        
        public static void LoaderCallBack()
        {
            SceneManager.LoadScene(targetScene.ToString());
        }
    }
}