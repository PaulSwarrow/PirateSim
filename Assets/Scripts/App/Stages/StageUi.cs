using System;
using Lib;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class StageUi : BaseComponent
    {
        public IStageGoalProvider GoalProvider;

        [SerializeField] private Text goalDescription;
        [SerializeField] private Text goalProgress;
        private void Update()
        {
            goalDescription.text = GoalProvider.GoalDescription;
            goalProgress.text = GoalProvider.GoalState;
        }
    }
}