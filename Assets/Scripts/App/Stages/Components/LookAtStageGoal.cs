using System;
using App.Ui.Views;
using Lib;
using ShipSystems;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Components
{
    public class LookAtStageGoal : BaseComponent, IStageGoalProvider
    {
        public bool GoalAchieved { get; private set; }
        public string GoalDescription => "Turn ship to the required direction";
        public string GoalState { get; private set; } = "";

        [SerializeField] private ColorizedUiGroup target;
        [SerializeField] private ShipEntity ship;
        [SerializeField] private float accurancy = 1f;
        [SerializeField] private float timeToAccept = 3;
        private float angleDelta;
        private Vector3 requiredDirection;

        private float currentTime = 0;

        private void Awake()
        {
            requiredDirection = target.transform.forward;
        }

        private void Update()
        {
            if (GoalAchieved) return;

            angleDelta = Vector3.Angle(ship.transform.forward, requiredDirection);
            target.transform.position = ship.transform.position;


            if (angleDelta < accurancy)
            {
                target.Color = Color.green;
                currentTime += Time.deltaTime;
                if (currentTime >= timeToAccept)
                {
                    GoalAchieved = true;
                    GoalState = "Complete!";
                }
                else
                {

                    GoalState = $"Keep this direction: {timeToAccept - currentTime:0.00} sec";
                }
            }
            else
            {
                target.Color = Color.red;
                target.gameObject.SetActive(true);
                currentTime = 0;
                GoalState = $"Progress: {(1 - angleDelta / 180) * 100:0.00}%";
            }
        }
    }
}