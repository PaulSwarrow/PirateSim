using System.Collections;
using App.AI;
using HutongGames.PlayMaker.Actions;
using UnityEngine;

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
            if (GameManager.CharacterHud.InteractionAvailable && Input.GetButtonDown("Action"))
            {
                if (GameManager.CharacterHud.InteractionObject.TryGetComponent<WorkableObject>(out var workableObject))
                {
                    if (workableObject.OccupyWorkplace(character, out var workplace))
                    {
                        
                        character.SetState(new WorkingCharacterState(workplace));
                    }
                }
            }
        }

        private IEnumerator WorkAt(WorkPlace workPlace)
        {
            var entry = new CharacterCutsceneState(workPlace.characterMotor, workPlace.entryScene);
            character.SetState(entry);
            yield return new WaitUntil(entry.IsComplete);
        }
    }
}