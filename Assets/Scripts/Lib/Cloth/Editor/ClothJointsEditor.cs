using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ClothJoints))]
public class ClothJointsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (DrawDefaultInspector())
        {
            
            serializedObject.ApplyModifiedProperties();
        }
        if (GUILayout.Button("Bake"))
        {
            ((ClothJoints) target).Bake();
        }
    }
}
