using System;
using Game.ShipSystems.Refactoring;
using Lib;
using UnityEngine;

namespace ShipSystems
{
    public class SailGroupView : BaseComponent
    {
        private SailView[] sails;
        private Animator animator;
        [SerializeField] private Transform rotationTarget;



        [NonSerialized] public SailGroupModel model;
        private readonly int DirectionName = Animator.StringToHash("Direction");


        private void Awake()
        {
            sails = GetComponentsInChildren<SailView>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        { 
            if(model == null) return;

            var l = Mathf.Min(sails.Length, model.State.sails.Length);//TODO better sol
            for (int i = 0; i < l; i++)
            {
                sails[i].Raised = model.State.sails[i].value > 0.1f; 
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