using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Data;
using Game.Actors.Ship;
using Lib;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace DefaultNamespace
{
    public class AppManager : BaseComponent
    {
        private static AppManager instance;
        private static string homeScene = "Main";
        private static Profile profile = new Profile();

        public static SailingConstantsConfig SailConstants => instance.sailConstants;

        public static void LoadStage(StageConfig stage)
        {
            SceneManager.LoadScene(stage.scene);
            Debug.Log("Scene loaded");
        }

        public static void GoHome() => SceneManager.LoadScene(homeScene);

        public static IEnumerable<StageConfig> GetStages() => instance.stages;

        [SerializeField] private StageConfig[] stages;
        [SerializeField] private SailingConstantsConfig sailConstants;
 
        private int tutorialLength;
        private HashSet<StageConfig> tutorialProgress = new HashSet<StageConfig>();

        private void Awake()
        {
            if (instance)
            {
                Destroy(gameObject);
                return;
            }

            homeScene = SceneManager.GetActiveScene().name;

            instance = this;
            DontDestroyOnLoad(gameObject);

            foreach (var stageConfig in stages)
            {
                if (stageConfig.tutorial) tutorialLength++;
            }
        }
    }
}