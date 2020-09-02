using System;
using DefaultNamespace.Data;
using Lib;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class AppManager : BaseComponent
    {
        private static string homeScene = SceneManager.GetActiveScene().name;

        private static Profile profile = new Profile();

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