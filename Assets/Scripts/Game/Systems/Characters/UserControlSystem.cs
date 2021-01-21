using Game.Actors.Character;
using Game.Actors.Character.Motors;
using Game.Interfaces;
using UnityEngine;

namespace Game.Systems.Characters
{
    /*
     * Implements user control for selected character
     */
    public class UserControlSystem : IGameSystem
    {
        public GameCharacter Character { get; private set; }

        public void Init()
        {
        }

        public void CreatePlayer(Vector3 position, Vector3 forward)
        {
            Character = GameManager.Characters.CreateCharacter(position, forward);

        }

        public void Start()
        {
            GameManager.UpdateEvent += Update;
        }

        public void Stop()
        {
            GameManager.UpdateEvent -= Update;
        }

        public void Update()
        {
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