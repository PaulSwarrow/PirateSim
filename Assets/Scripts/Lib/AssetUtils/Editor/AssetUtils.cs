using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class AssetUtils
{
    public static T CreateOrReplaceAsset<T> (T asset, string path) where T:Object{
        T existingAsset = AssetDatabase.LoadAssetAtPath<T>(path);
          
        if (existingAsset == null){
            AssetDatabase.CreateAsset(asset, path);
            existingAsset = asset;
        }
        else{
            EditorUtility.CopySerialized(asset, existingAsset);
        }
          
        return existingAsset;
    }
}
