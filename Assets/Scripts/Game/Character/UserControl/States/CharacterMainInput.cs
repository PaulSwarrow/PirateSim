using App.Navigation;
using UnityEngine;

namespace App.Character.UserControl
{
    public class CharacterMainInput : GameCharacterState
    {
        private Camera camera;
        private DynamicNavmeshAgent navigator;
        private CharacterMainMotor motor;

        public override void Start()
        {
            camera = Camera.main;
            navigator = character.agent.navigator;
            motor = character.agent.defaultMotor;
            motor.Enable(character.agent);
        }

        public override void Update()
        {
            var input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            var run = Input.GetButton("Run");
            input = Vector3.ClampMagnitude(input, 1); // fix for keyboard
            var jump = Input.GetButtonDown("Jump");

            var vector = camera.transform.TransformDirection(input);
            vector.y = 0; //compensate camera x-angle
            vector.Normalize();

            if (input.magnitude > 0)
            {
                
                var deltaQuaternion = Quaternion.FromToRotation(motor.Forward, vector);
                deltaQuaternion = Quaternion.Lerp(Quaternion.identity, deltaQuaternion, 0.2f);
                motor.Forward = deltaQuaternion * motor.Forward;
                motor.NormalizedVelocity = (Vector3.forward * (input.magnitude));
            }
            else
            {
                motor.NormalizedVelocity = Vector3.zero;
            }
            
            motor.Update();
        }

        public override void Stop()
        {
            motor.Disable();
        }
    }
}