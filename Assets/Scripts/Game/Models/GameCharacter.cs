using Game.Interfaces;
using UnityEngine;

namespace Game.Actors.Character
{
    /*
     * Character model
     * Links profile, input, ai and state machine
     * Exists all the time
     */
    public class GameCharacter
    {
        public GameCharacterActor actor;
        public ICharacterLiveArea livingArea;

        public Vector3 worldPosition => actor.transform.position;

    }
}