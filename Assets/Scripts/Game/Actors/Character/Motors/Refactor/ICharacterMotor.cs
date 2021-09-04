using UnityEngine;

namespace Game.Actors.Character.Motors.Settings
{
    public interface ICharacterMotor
    {
        void Enable();

        void Disable();

        void OnUpdate();
        void OnRootMotion();
        CharacterActorContext context { get; set; }
    }
}