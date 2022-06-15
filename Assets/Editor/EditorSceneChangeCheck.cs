using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class EditorSceneChangeCheck
{
    static EditorSceneChangeCheck()
    {
        UnityEditor.SceneManagement.EditorSceneManager.sceneOpened +=
            SceneOpenedCallback;
    }

    private static void SceneOpenedCallback(Scene scene, OpenSceneMode mode)
    {
        Debug.Log($"SCENE LOADED : {scene.path} , mode : {mode}");
    }
}