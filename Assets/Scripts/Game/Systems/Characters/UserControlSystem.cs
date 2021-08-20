using App.SceneContext;
using DI;
using Game.Actors.Character;
using Game.Actors.Character.Motors;
using Game.Interfaces;
using Game.Tools;
using UnityEngine;

namespace Game.Systems.Characters
{
    /*
     * Implements user control for selected character
     */
    //TODO single-entity system
    public class UserControlSystem : IGameSystem, IGameUpdateSystem
    {
        [Inject] private GameCharacterSystem _charactersSystem;  
        private ActorSelector<PlayerSpawn> spawnPoints = new ActorSelector<PlayerSpawn>();
        public GameCharacter Character { get; private set; }

        public void Init()
        {
        }

        public void Start()
        {
            if (spawnPoints.GetRandom(out var point))
            {
                var transform = point.transform;
                Character = _charactersSystem.CreateCharacter(transform.position, transform.forward);
            }
        }

        public void Stop()
        {
        }

        public void Update()
        {
            //TODO remove hardcode
            if(Character == null) return;
            
            var input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            var run = Input.GetButton("Run");
            input = Vector3.ClampMagnitude(input, 1); // fix for keyboard
            var jump = Input.GetButtonDown("Jump");

            
            var vector = Camera.main.transform.TransformDirection(input);
            vector.y = 0; //compensate camera x-angle
            vector = vector.normalized * input.magnitude;
            var move = vector;

            var motor = (CharacterMainMotor) Character.actor.motor;

            if (move.magnitude > 0)
            {
                motor.Look(move);
                motor.Move(move);
            }
        }
    }
}