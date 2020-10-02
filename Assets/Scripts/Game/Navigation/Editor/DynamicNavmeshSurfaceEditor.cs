using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

[CustomEditor(typeof(DynamicNavMeshSurface))]
public class DynamicNavmeshSurfaceEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Bake"))
        {
            var surface = (DynamicNavMeshSurface) target;
            
           var buildSources = new List<NavMeshBuildSource>();
     
            NavMeshBuilder.CollectSources(
                surface.transform,
                NavMesh.AllAreas,
                NavMeshCollectGeometry.RenderMeshes,
                0,
                new List<NavMeshBuildMarkup>(),
                buildSources);
     
            var navData = NavMeshBuilder.BuildNavMeshData(
                NavMesh.GetSettingsByID(0),
                buildSources,
                new Bounds(Vector3.zero, new Vector3(10000, 10000, 10000)),
                Vector3.down,
                Quaternion.Euler(Vector3.up));

            AssetUtils.CreateOrReplaceAsset(navData, "Assets/NavMeshes/" + target.name+".asset");
        }
    }
}
