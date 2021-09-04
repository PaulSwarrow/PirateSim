using DI;
using Lib.Navigation;
using UnityEngine;

namespace Game.Actors.Character.Motors.Settings.Impl
{
    public class RootMotionMotor : ICharacterMotor
    {
        public CharacterActorContext context { get; set; }

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
                context.transform.position += context.view.deltaPosition;
                context.transform.rotation *= context.view.deltaRotation;
        }

    }
}