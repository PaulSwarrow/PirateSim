using Game.Systems.Sea;
using Lib;

namespace Game.GDTools
{
    public class WindSetup : BaseComponent
    {
        public float Force;

        public float Angle;

        private void Start()
        {
        }

        private void Update()
        {
            Angle %= 360;
            GameManager.Wind.SetWind(Angle, Force);
        }
        
    }
}