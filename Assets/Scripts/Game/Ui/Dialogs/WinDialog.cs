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
        private ITimeSpanTracker timespanTracker;

        private void Awake()
        {
            timespanTracker = StageController.current.GetComponent<ITimeSpanTracker>();
            if (timespanTracker != null)
            {
                scoreTf.text = $"Your time: {timespanTracker.TimeSpanString}";
            } 
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