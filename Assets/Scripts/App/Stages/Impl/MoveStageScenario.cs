using System;
using System.Collections;
using System.Linq;
using Game;
using Game.Actors.Ship;
using Game.Systems;
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

        private ShipActor ship;
        private int progress;
        [SerializeField] private Part[] sequence;
        private Part currentStep;
        [SerializeField] private float windForce = 5;

        private void Start()
        {
            //TODO may be to early. Need to integrate in game manager lifecycle
            ship = GameManager.current.Get<ShipModelSystem>().All.First().actor;
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
            GameManager.current.Get<WindSystem>().SetWind(currentStep.wind, windForce);
        }
    }
}