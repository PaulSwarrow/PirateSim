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

        private int progress;

        private void Move(Npc npc, bool resume)
        {
            if (!resume) //recalculate from time to time
            {
                progress = 0;
                //TODO path based on navpoints!
                NavMesh.CalculatePath(npc.character.navPosition, npc.targetPosition.virtualPosition, NavMesh.AllAreas,
                    npc.path);
            }

            var motor = (CharacterMainMotor) npc.character.actor.motor;

            if (progress < npc.path.corners.Length)
            {
                var point = npc.path.corners[progress];
                if (Vector3.Distance(point, npc.character.navPosition) < npc.travelAccurancy)
                {
                    progress++;
                }
                else
                {
                    var movementVector = point - npc.character.navPosition;
                    //HOT-FIX!
                    movementVector = npc.character.actor.navigator.surface.Virtual2WorldDirection(movementVector);
                    motor.Forward = movementVector;
                    motor.NormalizedVelocity = Vector3.forward;
                }
            }
            else
            {
                motor.NormalizedVelocity = Vector3.zero;
            }
        }
    }
}