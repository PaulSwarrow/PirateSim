using System;
using Lib;
using UnityEngine;

namespace App.GDTools
{
    public class WindSetup : BaseComponent
    {
        public float Force;

        public float Angle;
        private WindSystem windSystem;

        private void Start()
        {
            windSystem = GameManager.current.GetSystem<WindSystem>();
        }

        private void Update()
        {
            Angle %= 360;
            windSystem.SetWind(Angle, Force);
        }
        
    }
}