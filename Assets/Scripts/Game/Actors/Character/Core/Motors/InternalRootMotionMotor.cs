using DI;
using UnityEngine;

namespace Game.Actors.Character.Core.Motors
{
    public class InternalRootMotionMotor : ICharacterMotor
    {
        private Transform _viewTransform;
        public void Enable()
        {
            _viewTransform = context.view.transform;
        }

        public void Disable()
        {
            
        }

        public void OnUpdate()
        {
            
        }

        public void OnRootMotion()
        {
            _viewTransform.localPosition += context.view.deltaPosition;
            _viewTransform.localRotation *= context.view.deltaRotation;
        }

        public CharacterActorContext context { get; set; }
    }
}