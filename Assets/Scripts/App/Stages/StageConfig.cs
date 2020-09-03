using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "StageConfig", menuName = "Game/StageConfig", order = 10000)]
    public class StageConfig :ScriptableObject
    {
        public string scene;
        public bool tutorial;
    }
}