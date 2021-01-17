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

        public Vector3 Position => actor.transform.position;
    }
}