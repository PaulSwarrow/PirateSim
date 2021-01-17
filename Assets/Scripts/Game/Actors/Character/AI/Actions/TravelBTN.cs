using Game.Actors.Character.Motors;
using Game.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Actors.Character.AI.Hardcode
{
    public class TravelBTN : BehaviourTreeCondition
    {
        public TravelBTN()
        {
            Condition = npc => npc.currentWorkPlace;
            BranchA = new ExitWorkPlaceBTN();
            BranchB = new BehaviourTreeInstantAction
            {
                action = Move
            };
        }

        private NavMeshPath path;
        private int progress;

        private void Move(Npc npc, bool resume)
        {
            if (!resume) //recalculate from time to time
            {
                progress = 0;
                NavMesh.CalculatePath(npc.character.Position, npc.targetPosition, NavMesh.AllAreas, path);
            }

            var motor = (CharacterMainMotor) npc.character.actor.motor;

            if (progress < path.corners.Length)
            {
                var point = path.corners[progress];
                if (Vector3.Distance(point, npc.character.Position) < npc.travelAccurancy)
                {
                    progress++;
                }
                else
                {
                    var movementVector = point - npc.character.Position;
                    motor.Forward = movementVector;
                    motor.NormalizedVelocity = movementVector.normalized;
                }
            }
            else
            {
                motor.NormalizedVelocity = Vector3.zero;
            }
        }
    }
}