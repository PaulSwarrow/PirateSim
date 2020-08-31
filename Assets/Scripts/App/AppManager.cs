using System;
using Lib;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class AppManager : BaseComponent
    {
        private string homeScene;
        private void Awake()
        {
            homeScene = SceneManager.GetActiveScene().name;
        }

        public void LoadStage(StageConfig stage)
        {
            SceneManager.LoadScene(stage.name);
        }

        public void GoHome()
        {
            SceneManager.LoadScene(homeScene);
        }
        
    }
}