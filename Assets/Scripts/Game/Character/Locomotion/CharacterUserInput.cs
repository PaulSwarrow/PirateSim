using System;
using Lib;
using UnityEngine;

namespace App.Character.Locomotion
{
    public class CharacterUserInput : BaseComponent
    {
        private Camera camera;
        private ICharacterMotor motor;

        private void Awake()
        {
            camera = Camera.main;
            motor = GetComponent<ICharacterMotor>();
        }

        private void FixedUpdate()
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


                motor.Move(motor.Forward * (input.magnitude * Time.fixedDeltaTime));
            }
        }
    }
}