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
                ((npc) => Reset || Vector3.Distance(npc.targetPosition.virtualPosition, npc.character.navPosition) < npc.travelAccurancy,
                    new FindRandomPlaceBTN()),
            };

            defaultBehaviour = new TravelBTN();
        }
    }
}