using App.Character.AI;
using Game.Character.Statemachine;

namespace App.Character
{
    /*
     * Character state-machine's state
     * Connect user input or ai to an actual character's motor    
     */
    public abstract class GameCharacterState
    {
        public GameCharacter character;
        protected CharacterStateMachine stateMachine;

        public void Init(GameCharacter character, CharacterStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
            this.character = character;
        }

        public abstract void Start();

        public abstract void Update();

        public abstract void Stop();

        public abstract void ReceiveTask(CharacterStatemachineTask task);
    }
    public abstract class GameCharacterState<TApi> : GameCharacterState where TApi: BaseStateApi, new()
    {
        protected TApi api { get;} = new TApi();


        public override void ReceiveTask(CharacterStatemachineTask task)
        {
            task.Update(api);
        }
    }

    public interface IStateWithData<TData>
    {
        void SetData(TData data);

    }
}