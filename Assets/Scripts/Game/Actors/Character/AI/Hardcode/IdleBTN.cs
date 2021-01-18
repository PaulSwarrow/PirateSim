namespace Game.Actors.Character.AI.Hardcode
{
    public class IdleBTN : BehaviourTreeInstantAction
    {
        public IdleBTN()
        {
            action = Wait;
        }

        private void Wait(Npc npc)
        {
        }
    }
}