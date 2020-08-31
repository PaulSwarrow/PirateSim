using System;
using Lib;
using UnityEngine;

namespace DefaultNamespace
{
    public class StageController : BaseComponent
    {
        [SerializeField] private StageUi ui;
        private void Awake()
        {
            ui.GoalProvider = GetComponent<IStageGoalProvider>();

        }
        
        
    }
}