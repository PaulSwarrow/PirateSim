using Lib;
using UnityEngine;

namespace Game.Actors.Ship.View
{
    public class SailView  : BaseComponent, ISailView
    {
        private Animator animator;
        private static readonly int FlagName = Animator.StringToHash("Raised");


        private void Awake()
        {
            animator = GetComponent<Animator>();
        }


        public float Progress
        {
            set => animator.SetBool(FlagName, value > 0.1f);
        }
        public Vector3 Wind { get; set; }
    }
}