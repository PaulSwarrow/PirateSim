using System;
using DefaultNamespace;
using Lib;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Ui.Dialogs
{
    public class WinDialog : BaseComponent
    {
        [SerializeField] private Button homeBtn;
        [SerializeField] private Text scoreTf;

        private void Awake()
        {
            homeBtn.onClick.AddListener(AppManager.GoHome);
        }

        private void OnEnable()
        {
            StageUi.RequireCursor(this);
        }

        private void OnDisable()
        {
            StageUi.LoseCursor(this);
        }
    }
}