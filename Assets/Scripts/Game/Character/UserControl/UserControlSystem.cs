using System;
using App.AI;
using Lib;

namespace App.Character.UserControl
{
    /*
     * Implements user control for selected character
     */
    public class UserControlSystem : BaseComponent
    {
        private GameCharacter character;

        private void Start()
        {
            character = GameCharacterSystem.First();
            character.SetState(new CharacterMainInput());

        }

    }
}