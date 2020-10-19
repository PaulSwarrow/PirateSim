namespace App.Character
{
    /*
     * Character state-machine's state
     * Connect user input or ai to an actual character's motor    
     */
    public abstract class GameCharacterState
    {
        public GameCharacter character;

        public void Init(GameCharacter character)
        {
            this.character = character;
        }

        public abstract void Start();

        public abstract void Update();

        public abstract void Stop();

    }
    public abstract class GameCharacterState<TData> : GameCharacterState
    {
        public abstract void SetData(TData data);

    }
}