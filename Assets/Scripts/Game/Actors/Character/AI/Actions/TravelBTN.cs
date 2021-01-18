using Game.Actors.Character.Motors;
using Game.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

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
                start = StartMovement,
                action = Move,
                stop = StopMovement
            };
        }


        private CharacterMainMotor motor;
        private void StartMovement(Npc npc)
        {
            //TODO path based on navpoints!
            NavMesh.CalculatePath(npc.character.navPosition, npc.targetPosition.virtualPosition, NavMesh.AllAreas,
                npc.path);

            motor = (CharacterMainMotor) npc.character.actor.motor;
            Assert.IsNotNull(motor);
            motor.Travel(npc.targetPosition);
        }

        private void Move(Npc npc)
        {
            motor.Look(motor.WorldVelocity);
        }

        private void StopMovement(Npc npc)
        {
            motor.Stop();
        }
    }
}