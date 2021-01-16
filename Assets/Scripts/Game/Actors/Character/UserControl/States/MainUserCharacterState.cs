using Game.Actors.Character.Interactions;
using Game.Actors.Character.Motors;
using Game.Actors.Character.States;
using UnityEngine;

namespace Game.Actors.Character.UserControl.States
{
    public class MainUserCharacterState : GameCharacterState
    {
        private Camera camera;
        private CharacterMainMotor motor;

        public override void Start()
        {
            camera = Camera.main;
            motor = character.agent.defaultMotor;
            character.agent.SetMotor(motor);
            GameManager.CharacterHud.Active = true;
            GameManager.CharacterHud.WorkEvent += OnUseWorkplace;
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
            
        }

        private void OnUseWorkplace(WorkableObject workableObject)
        {
            if (workableObject.OccupyWorkplace(character, out var workplace))
            {
                // stateMachine.RequireState<WorkingUserCharacterState, WorkPlace>(workplace);
            }
        }

        public override void Stop()
        {
            GameManager.CharacterHud.WorkEvent -= OnUseWorkplace;
            GameManager.CharacterHud.Active = false;
            
        }

        public override void ReceiveTask(CharacterStatemachineTask task)
        {
            
        }
    }
}