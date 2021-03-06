using Game.Actors.Character.Interactions;
using Game.Interfaces;
using Game.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Actors.Character.AI
{
    public class Npc
    {
        public readonly GameCharacter character;
        public ICharacterLiveArea liveArea;
        public WorkPlace currentWorkPlace;
        public WorkPlace targetWorkPlace;
        public NavPoint targetPosition;
        public float travelAccurancy = .1f;
        public NavMeshPath path = new NavMeshPath();


        public Npc(GameCharacter gameCharacter)
        {
            character = gameCharacter;
        }

        public Vector3 Position => character.worldPosition;
    }
}