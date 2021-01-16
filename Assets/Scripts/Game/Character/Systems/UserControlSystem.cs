using App.Interfaces;
using UnityEngine;

namespace App.Character.UserControl
{
    /*
     * Implements user control for selected character
     */
    public class UserControlSystem : IGameSystem
    {
        public GameCharacter character { get; private set; }

        public void Init()
        {
        }

        public void Start()
        {
            character = GameCharacterSystem.First();
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

            var motor = (CharacterMainMotor) character.agent.motor;

            if (move.magnitude > 0)
            {
                var deltaQuaternion = Quaternion.FromToRotation(motor.Forward, move);
                deltaQuaternion = Quaternion.Lerp(Quaternion.identity, deltaQuaternion, 0.2f);
                motor.Forward = deltaQuaternion * motor.Forward;
                motor.NormalizedVelocity = (Vector3.forward * (move.magnitude));
            }
            else
            {
                motor.NormalizedVelocity = Vector3.zero;
            }
        }
    }
}