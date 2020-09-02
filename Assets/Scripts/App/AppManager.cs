using System;
using Lib;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class AppManager : BaseComponent
    {
        private static string homeScene = SceneManager.GetActiveScene().name;
     

        public static void LoadStage(StageConfig stage)
        {
            SceneManager.LoadScene(stage.name);
        }

        public static void GoHome()
        {
            SceneManager.LoadScene(homeScene);
        }
        
    }
}