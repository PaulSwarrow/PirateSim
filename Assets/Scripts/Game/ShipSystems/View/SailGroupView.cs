using System;
using App;
using Game.ShipSystems.Sails.Data;
using Lib;
using UnityEngine;

namespace ShipSystems
{
    public class SailGroupView : BaseComponent
    {
        private ISailView[] sails;
        private Animator animator;
        [SerializeField] private Transform rotationTarget;



        [NonSerialized] public SailGroupModel model;
        private readonly int DirectionName = Animator.StringToHash("Direction");


        private void Awake()
        {
            sails = GetComponentsInChildren<ISailView>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        { 
            if(model == null) return;

            for (int i = 0; i < sails.Length; i++)
            {
                var view = sails[i];
                view.Progress = model.State.sails[Mathf.Min(i, model.State.sails.Length-1)].value;
                view.Wind = WindSystem.Wind;
            }
            
            if (animator)
            {
                animator.SetFloat(DirectionName, -model.State.angle);
            }

            if (rotationTarget)
            {
                rotationTarget.localEulerAngles = Vector3.up * model.State.angle;
            }
        }
    }
}