using System;
using Lib;
using UnityEngine;

namespace ShipSystems
{
    public class SailView  : BaseComponent
    {
        private Animator animator;
        private static readonly int FlagName = Animator.StringToHash("Raised");


        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public bool Raised
        {
            set => animator.SetBool(FlagName, value);
            get => animator.GetBool(FlagName);
        }



    }
}