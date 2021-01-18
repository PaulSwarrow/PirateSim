using System;
using Game.Actors.Ship.Sails;
using Game.Systems.Sea;
using Lib;
using UnityEngine;

namespace Game.Actors.Ship.View
{
    public class SailGroupView : BaseComponent
    {
        private ISailView[] sails;
        [SerializeField] private float angleMultiplier = 1;
        [SerializeField] private Transform rotationTarget;



        [NonSerialized] public SailGroupModel model;
        private readonly int DirectionName = Animator.StringToHash("Direction");


        private void Awake()
        {
            sails = GetComponentsInChildren<ISailView>();
        }

        private void Update()
        { 
            if(model == null) return;

            for (int i = 0; i < sails.Length; i++)
            {
                var view = sails[i];
                view.Progress = model.State.sails[Mathf.Min(i, model.State.sails.Length-1)].value;
                view.Wind = GameManager.Wind.Force;
            }

            if (rotationTarget)
            {
                rotationTarget.localEulerAngles = Vector3.up * (model.State.angle * angleMultiplier);
            }
        }
    }
}