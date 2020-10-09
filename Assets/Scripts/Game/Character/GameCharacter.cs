namespace App.Character
{
    /*
     * Character model
     * Links profile, input, ai and state machine
     */
    public class GameCharacter
    {
        public GameCharacterAgent agent;
        
        private GameCharacterState state;

        public void SetState(GameCharacterState state)
        {
            if(state == this.state) return;
            if(this.state != null) this.state.Stop();
            this.state = state;

            state.Init(this);
            state.Start();
        }

        public void Update()
        {
            state.Update();
            
        }


    }
}