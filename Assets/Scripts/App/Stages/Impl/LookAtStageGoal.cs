using System;
using App;
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
        public bool GoalAchieved { get; protected set; }
        public string GoalDescription { get; protected set; }
        public string GoalState { get; protected set; } = "";


        [SerializeField] private float[] Angles;
        [SerializeField] private ColorizedUiGroup target;
        [SerializeField] private ShipEntity ship;
        [SerializeField] private float accurancy = 1f;
        [SerializeField] private float timeToAccept = 3;
        private float angleDelta;

        private float currentTime = 0;
        private int currentStep;

        private void Start()
        {
            ship = GameManager.current.currentShip;
        }

        private void Update()
        {
            GoalDescription = $"Turn the ship to the required direction. ({currentStep}/{Angles.Length})";
            if (GoalAchieved) return;
            var targetAngle = Angles[currentStep];
            var q = Quaternion.Euler(0, targetAngle, 0);
            target.transform.rotation = q;
            angleDelta = Vector3.Angle(ship.transform.forward, q * (Vector3.forward));
            target.transform.position = ship.transform.position;


            if (angleDelta < accurancy)
            {
                target.Color = Color.green;
                currentTime += Time.deltaTime;
                if (currentTime >= timeToAccept)
                {
                    currentStep++;
                    if (currentStep < Angles.Length) return;
                    GoalAchieved = true;
                    GoalState = "";
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