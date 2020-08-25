using System;
using Lib;
using UnityEngine;

namespace App
{
    public class WaterSystem:BaseComponent
    {
        public static WaterSystem instance { get; private set; }
        [SerializeField]private float amplitude = 1 ;
        [SerializeField] private float lenght = 2;
        [SerializeField] private float speed = 1;
        private float offset;

        private void Awake()
        {
            instance = this;
        }

        private void Update()
        {
            offset += speed * Time.deltaTime;
        }

        public float GetWaterHeight(Vector3 position)
        {
            return 0;
            return OceanAdvanced.GetWaterHeight(position);
        }
    }
}