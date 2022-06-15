using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class EditorSceneChangeCheck
{
    private static string currentScene;

    static EditorSceneChangeCheck()
    {
        currentScene = EditorApplication.currentScene;
        EditorApplication.hierarchyWindowChanged += hierarchyWindowChanged;
    }

    private static void hierarchyWindowChanged()
    {
        if (currentScene != EditorApplication.currentScene)
        {
            //a scene change has happened
            Debug.Log("Last Scene: " + currentScene);
            currentScene = EditorApplication.currentScene;
        }
    }
}