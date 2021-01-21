using System;
using System.Collections.Generic;
using Game.Actors.Character.AI.Actions;
using Game.Actors.Character.Interactions;
using UnityEngine;

namespace Game.Actors.Character.AI.Hardcode
{
    public class ChillBTN : BehaviourTreeSwitcher
    {

        public ChillBTN()
        {
            cases = new List<(Condition condition, IBehaviourTreeNode handler)>
            {
                ((npc) => Reset || npc.targetWorkPlace == null, new FindChillingPlace()),
                (npc=> npc.targetWorkPlace, new BehaviourTreeInstantAction{action = SetTargetPosition}),
                (npc=> npc.targetPosition == null, new FindRandomPlaceBTN()),
            };

            defaultBehaviour = new TravelBTN();
        }

        private void SetTargetPosition(Npc npc)
        {
            npc.liveArea.TryFindPlace(npc.targetWorkPlace.EnterPosition, 10, out npc.targetPosition);
        }
    }
}