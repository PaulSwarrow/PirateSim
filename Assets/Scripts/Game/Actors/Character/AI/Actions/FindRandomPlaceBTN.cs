using System;

namespace Game.Actors.Character.AI.Actions
{
    public class FindRandomPlaceBTN : BehaviourTreeInstantAction
    {
        public FindRandomPlaceBTN()
        {
            action = FindPlace;
        }

        private void FindPlace(Npc npc, bool resume)
        {
            npc.liveArea.TryFindPlace(10, out npc.navTarget);

        }
    }
}