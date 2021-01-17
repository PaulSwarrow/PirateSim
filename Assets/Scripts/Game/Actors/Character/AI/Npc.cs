using Game.Actors.Character.Interactions;
using Game.Interfaces;
using Game.Navigation;
using UnityEngine;

namespace Game.Actors.Character.AI
{
    public class Npc
    {
        public readonly GameCharacter character;
        public ICharacterLiveArea liveArea;
        public WorkPlace currentWorkPlace;
        public WorkPlace targetWorkPlace;
        public NavPoint navTarget;
        public Vector3 targetPosition;
        public float travelAccurancy = .1f;


        public Npc(GameCharacter gameCharacter)
        {
            character = gameCharacter;
        }

        public Vector3 Position => character.Position;
    }
}