using System;
using DefaultNamespace;
using Lib;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Ui
{
    public class GameMenu : BaseComponent
    {
        [SerializeField] private Button resume;
        [SerializeField] private Button restart;
        [SerializeField] private Button home;

        private void Awake()
        {
            resume.onClick.AddListener(Resume);
            restart.onClick.AddListener(Restart);
            restart.onClick.AddListener(AppManager.GoHome);
        }

        private void OnEnable()
        {
            StageUi.RequireCursor(this);
        }

        private void OnDisable()
        {
            StageUi.LoseCursor(this);
        }

        private void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void Resume()
        {
            gameObject.SetActive(false);
        }
    }
}