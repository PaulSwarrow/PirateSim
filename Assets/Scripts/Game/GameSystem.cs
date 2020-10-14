namespace App
{
    public abstract class GameSystem
    {
        protected GameManager manager => GameManager.current;
        public  virtual void Start() {}

        public abstract void Update();

        public virtual void Stop() {}
    }
}