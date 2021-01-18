namespace Game.Actors.Ship.Sails.Data
{
    public class SailOrder
    {
        public SailGroupModel sails;
        public SailGroupTask task;

        public bool IsEmpty() => sails.Task.IsEqual(task);
    }
}