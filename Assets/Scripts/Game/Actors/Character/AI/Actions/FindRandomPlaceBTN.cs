using UnityEngine;

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
            var offset = Quaternion.Euler(0, Random.Range(0, 360), 0) * Vector3.forward * Random.Range(5f, 10);
            var searchPosition = npc.character.worldPosition + offset;
            npc.liveArea.TryFindPlace(searchPosition, 10, out var position);
            npc.targetPosition = position;
        }
    }
}