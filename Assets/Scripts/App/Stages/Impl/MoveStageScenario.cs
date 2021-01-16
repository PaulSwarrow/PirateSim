using System;
using System.Collections;
using Game;
using Game.Actors.Ship;
using Game.Systems.Sea;
using Game.Tools;
using Game.Ui.Dialogs;
using Lib;
using UnityEngine;

namespace DefaultNamespace.Components
{
    public class MoveStageScenario : BaseStageScenario, IStageGoalProvider
    {
        [Serializable]
        private class Part
        {
            public float wind;
            public GameTrigger trigger;
            public TutorialDialog[] tutorial;
        }

        public bool GoalAchieved { get; private set; }
        public string GoalDescription { get; private set; }
        public string GoalState { get; private set; }

        private ShipEntity ship;
        private int progress;
        [SerializeField] private Part[] sequence;
        private Part currentStep;
        [SerializeField] private float windForce = 5;

        private void Start()
        {
            ship = GameManager.current.currentShip;
            foreach (var part in sequence)
            {
                part.trigger.gameObject.SetActive(false);
                part.trigger.EnterEvent += OnTrigger;
            }

            StartCoroutine(Tutorial(sequence[0].tutorial));
        }

        private void OnTrigger(GameTrigger trigger, Collider collider)
        {
            ship.FullStop();
            currentStep.trigger.gameObject.SetActive(false);
            progress++;
        }

        private void Update()
        {
            GoalState = $"{progress}/{sequence.Length}";
            if (progress >= sequence.Length)
            {
                GoalDescription = "Complete!";
                GoalAchieved = true;
                return;
            }

            GoalDescription = "Reach highlighted area";
            currentStep = sequence[progress];
            currentStep.trigger.gameObject.SetActive(true);
            GameManager.current.GetSystem<WindSystem>().SetWind(currentStep.wind, windForce);
        }
    }
}