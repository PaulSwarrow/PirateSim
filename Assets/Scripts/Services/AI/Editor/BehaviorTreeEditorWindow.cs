using System;
using Services.AI.Configs;
using Services.AI.Interfaces;
using UnityEditor;

namespace Services.AI.Editor
{
    public class BehaviorTreeEditorWindow : EditorWindow
    {
        public void Show<T>(BehaviourTreeConfig<T> config) where T : IBehaviorDataProvider, new()
        {
            
        }
        private void OnEnable()
        {
            
        }

        private void OnGUI()
        {
            
        }
    }
}