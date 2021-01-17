namespace Game.Actors.Character.AI.Hardcode
{
    public class NpcMind : NpcBehaviourTree
    {
        public NpcMind()
        {
            rootNode = new ChillBTN();
        }
    }
}