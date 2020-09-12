using System.Collections.Generic;
using Game.ShipSystems.Sails;
using Lib.UnityQuickTools.Collections;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ShipSailsController))]
public class ShipSailsControllerEditor : Editor
{
    private ShipSailsController _target;
    private HashSet<string> missedViews = new HashSet<string>();


    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        base.OnInspectorGUI();
        if (EditorGUI.EndChangeCheck() && _target != null) return;
        missedViews.Clear();
        _target = (ShipSailsController) target;
        foreach (var config in _target.config.sails)
        {
            if (!_target.sails.TryFind(item => item.name == config.name, out _))
            {
                missedViews.Add(config.name);
            }
        }

        if (missedViews.Count > 0)
            EditorGUILayout.LabelField($"Missed sail views: {string.Join(", ", missedViews)}");
    }
}