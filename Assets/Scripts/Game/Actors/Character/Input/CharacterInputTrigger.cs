using Lib.Tools;

namespace Game.Actors.Character.StateMachine
{
    public static class CharacterInputTrigger
    {
        private static uint _count;
        public static readonly int StartTravel = Add();

        private static int Add()
        {
            var value = GameMath.Pow(2, _count);
            _count++;
            return value;
        }
    }
}