using System;
using App.AI;
using Lib;

namespace App.Character.UserControl
{
    /*
     * Implements user control for selected character
     */
    public class UserControlSystem : GameSystem
    {
        public GameCharacter character { get; private set; }
        

        public override void Start()
        {
            character = GameCharacterSystem.First();
            character.SetState(new CharacterMainInput());

        }

        public override void Update()
        {
            
        }
    }
}