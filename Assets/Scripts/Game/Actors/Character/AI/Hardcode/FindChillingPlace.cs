namespace Game.Actors.Character.AI.Hardcode
{
    public class FindChillingPlace : BehaviourTreeInstantAction
    {
        public FindChillingPlace()
        {
            action = FindPlace;
        }

        private void FindPlace(Npc npc)
        {
            npc.liveArea.TryFindWorkPlace(place => !place.Occupied && place.AllowChilling, out npc.targetWorkPlace);
        }
    }
}