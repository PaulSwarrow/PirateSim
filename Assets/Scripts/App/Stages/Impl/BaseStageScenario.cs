using System.Collections;
using Game.Ui;
using Game.Ui.Dialogs;
using Lib;
using UnityEngine;

namespace DefaultNamespace.Components
{
    public class BaseStageScenario : BaseComponent
    {
        
        protected IEnumerator Tutorial(TutorialDialog[] sequence)
        {
            foreach (var prefab in sequence)
            {
                var dialog = Instantiate(prefab, StageUi.current.transform);
                yield return new WaitUntil(() => dialog.Complete);
            }
        }
    }
}