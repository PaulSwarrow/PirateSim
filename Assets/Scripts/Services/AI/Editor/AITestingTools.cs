using System.Collections;
using System.Collections.Generic;
using JsonSubTypes;
using Newtonsoft.Json;
using Services.AI;
using UnityEditor;
using UnityEngine;

public static class AITestingTools 
{
    [MenuItem("Dev/AI test")]
    private static void TestJson()
    {
        var path = "Assets/Scripts/Services/AI/AIConfigTest 1.json";
        var asset = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
        var obj = JsonConvert.DeserializeObject<BaseBehaviourTreeNode>(asset.text);
        Debug.Log(obj);


    }
    
}
