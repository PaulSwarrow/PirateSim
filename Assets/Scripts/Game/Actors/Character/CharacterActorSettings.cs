using System;
using UnityEngine;

namespace Game.Actors.Character
{
    [Serializable]
    public class CharacterActorSettings
    {
        public RuntimeAnimatorController animator;
        public float walkSpeed;
    }
}