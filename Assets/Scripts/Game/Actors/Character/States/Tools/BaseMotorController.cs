namespace Game.Actors.Character.States.Tools
{
    public abstract class BaseMotorController
    {
    }

    public abstract class BaseMotorController<TMotor> : BaseMotorController where TMotor : CharacterMotor
    {
        protected TMotor motor;

        public virtual void Start()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void Stop()
        {
        }
    }
}