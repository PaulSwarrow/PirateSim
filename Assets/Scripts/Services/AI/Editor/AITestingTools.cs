using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace Services.AI.Editor
{
    public static class AITestingTools 
    {
        [MenuItem("Dev/AI test")]
        private static void TestJson()
        {
            var path = "Assets/Scripts/Services/AI/AIConfigTest 1.json";
            var asset = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
            var obj = JsonConvert.DeserializeObject<BaseBehaviorTreeNode>(asset.text);
            Debug.Log(obj);


        }
    
    }
}
