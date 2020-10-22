using System.Collections;
using System.Diagnostics;
using App.AI;
using App.Character.AI.States;
using UnityEngine;

namespace App.Character.AI.Tasks
{
    public class ChillTask
    {
        private CharacterStateMachine statemachine;

        
        public IEnumerator Execute()
        {
            WorkPlace workPlace;
            while (!SearchForChillPlace(out workPlace))
            {
                yield return BoredWaiting();
            }

            yield return Travel(workPlace);

            yield return Chill(workPlace);

        }

        private bool SearchForChillPlace(out WorkPlace workPlace)
        {
            //TODO character context area
            workPlace = Object.FindObjectOfType<WorkPlace>();
            return true;
        }
        private IEnumerator BoredWaiting()
        {
            statemachine.RequireState<NpcBoredIdleState>();
            yield return null;
        }
        
        private IEnumerator Travel(WorkPlace workPlace)
        {
            var complete = false;
            statemachine.RequireState<NpcTravelState, NpcTravelState.Data>(new NpcTravelState.Data
            {
                worldPosition = workPlace.entryScene.transform.position,
                CompleteHandler = ()=> complete = true
            });
            
            yield return new WaitUntil(()=> complete);
        }

        private IEnumerator Chill(WorkPlace workPlace)
        {
            statemachine.RequireState<NpcChillState, WorkPlace>(workPlace);
            yield break;
        }


    }
}