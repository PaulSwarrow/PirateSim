using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Data;
using Lib;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace DefaultNamespace
{
    public class AppManager : BaseComponent
    {
        private static AppManager instance;
        private static string homeScene;
        private static Profile profile = new Profile();

        public static void LoadStage(StageConfig stage)
        {
            SceneManager.LoadScene(stage.scene);
            Debug.Log("Scene loaded");
        }

        public static void GoHome() => SceneManager.LoadScene(homeScene);

        public static IEnumerable<StageConfig> GetStages() => instance.stages;

        [SerializeField] private StageConfig[] stages;

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