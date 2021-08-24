using App.Configs;
using Services.AI.Configs;
using UnityEditor;

namespace Services.AI.Editor
{
    [CustomEditor(typeof(NpcBehaviorConfig))]
    public class BehaviourTreeEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
        }
    }
}