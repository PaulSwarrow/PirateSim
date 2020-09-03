using System;
using App;
using Lib;
using UnityEngine;

namespace DefaultNamespace
{
    public class StageController : BaseComponent
    {
        [SerializeField] private StageUi ui;

        public static StageController current { get; private set; }

        private void Awake()
        {
            ui.GoalProvider = GetComponent<IStageGoalProvider>();
        }

        private void OnEnable()
        {
            current = this;
        }

        private void OnDisable()
        {
            current = null;
        }
    }
}