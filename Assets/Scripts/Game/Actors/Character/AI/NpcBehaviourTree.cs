namespace Game.Actors.Character.AI
{
    public class NpcBehaviourTree
    {
        public IBehaviourTreeNode rootNode;
        private Npc npc;
        
        //TODO downfall update for pause|resume option?
        //TODO stop|dispose feature?

        public void Start(Npc npc)
        {
            this.npc = npc;
            Next();
        }

        private void Next()
        {
            rootNode.Execute(npc, Next);
        }
    }
}