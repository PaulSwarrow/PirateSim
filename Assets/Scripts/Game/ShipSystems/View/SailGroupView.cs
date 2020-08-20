using System;
using Lib;
using Lib.Tools.Data;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace ShipSystems
{
    
    public class SailGroupView : BaseComponent
    {
        private SailView[] sails;
        private Animator animator;

        private int currentValue;

        private SailDirection Direction;

        [NonSerialized] public SailGroup model;
        private readonly int DirectionName = Animator.StringToHash("Direction");


        private void Awake()
        {
            sails = GetComponentsInChildren<SailView>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        { 
            if(model == null)return;
            
            currentValue = Mathf.Clamp(model.Value, 0, sails.Length);
            for (int i = 0; i < sails.Length; i++)
            {
                sails[i].Raised = i < model.Value;
            }

            if (animator)
            {
                animator.SetFloat(DirectionName, -model.Options[model.Angle]);
            }
        }
    }
}