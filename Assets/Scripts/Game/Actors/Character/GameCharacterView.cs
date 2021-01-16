using System;
using Lib;
using UnityEngine;

namespace Game.Actors.Character
{
    /*
     * Visual representation of the character
     * provides api for higher logic
     */
    public class GameCharacterView : BaseComponent
    {
        public event Action MoveEvent;
        public Animator animator { get; private set; }
        public Vector3 deltaPosition { get; private set; }
        public Quaternion deltaRotation { get; private set; }

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        
        private void OnAnimatorMove()
        {
            deltaPosition = animator.deltaPosition;
            deltaRotation = animator.deltaRotation;
            MoveEvent?.Invoke();
        }
    }
}