using Lib.Tools;

namespace Game.Actors.Character.Input
{
    public class CharacterInputFlag
    {
        private static uint _count;
        public static readonly int Run = Add();

        private static int Add()
        {
            var value = GameMath.Pow(2, _count);
            _count++;
            return value;
        }
    }
}