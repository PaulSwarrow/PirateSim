using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace App.Character.AI.States
{
    public class NpcTravelState : GameCharacterState<NpcTravelState.Data>
    {
        public class Data
        {
            public Vector3 worldPosition;
            public Action CompleteHandler;
        }
        private static NavMeshPath GetPath(GameCharacter character, Vector3 targetPosition, DynamicNavMeshSurface surface = null)
        {
            if (surface)
            {
                if (surface.virtualNavmesh.SamplePosition(targetPosition, out var hit, 4))
                {
                    targetPosition = hit.position;
                }
                else
                {
                    throw new Exception("Position is not reachable in the provided surface");
                }
            }

            return character.agent.navigator.GetPath(targetPosition);
        }


        private NavMeshPath path;
        private CharacterMainMotor motor;

        public override void Start()
        {
            path = GetPath(character, data.worldPosition, character.agent.navigator.surface);
            motor = character.agent.defaultMotor;
            character.agent.SetDefaultMotor();
            GameManager.current.StartCoroutine(Coroutine());
        }

        public override void Update()
        {
            
        }

        private IEnumerator Coroutine()
        {
            for (int i = 0; i < path.corners.Length; i++)
            {
                var nextPoint = path.corners[i];
                
                motor.Forward = nextPoint - character.agent.navigator.navPosition;
                motor.NormalizedVelocity = Vector3.forward;
                while (Vector3.Distance(character.agent.navigator.navPosition, nextPoint) > 0.1f)
                {
                    yield return null;
                }
            }
            motor.NormalizedVelocity = Vector3.zero;
            data.CompleteHandler?.Invoke();
        }

        public override void Stop()
        {
            
        }
    }
}