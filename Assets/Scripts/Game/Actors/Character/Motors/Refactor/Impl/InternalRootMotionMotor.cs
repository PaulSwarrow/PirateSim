using DI;
using Lib.Navigation;
using UnityEngine;

namespace Game.Actors.Character.Motors.Settings.Impl
{
    public class InternalRootMotionMotor : ICharacterMotor
    {

        public void Enable()
        {
        }

        public void Disable()
        {
            
        }

        public void OnUpdate()
        {
            
        }

        public void OnRootMotion()
        {
            context.transform.localPosition += context.view.deltaPosition;
            context.transform.localRotation *= context.view.deltaRotation;
        }

        public CharacterActorContext context { get; set; }
    }
}