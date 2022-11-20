using System;
using System.Collections;
using System.Collections.Generic;
using JamOff.Scripts.Managers;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class AdditiveTool : EditorWindow
{
    private List<Scene> sceneList = new List<Scene>();
    private string scenename = "";
    private string[] terminations = new string[] { "_Logic.unity", "_Layout.unity", "_Decoration.unity", "_Lighting.unity" };

    [MenuItem("JamOff Tools/Additive")]
    public static void ShowWindow() => GetWindow(typeof(AdditiveTool));


    private void OnGUI()
    {
        GUILayout.Label("Scene additive Manager", EditorStyles.boldLabel);


        if (EditorSceneManager.sceneCount == 1)
        {
            scenename = EditorGUILayout.TextField("Scene Name", scenename);

            if (GUILayout.Button("Create"))
            {
                CreateScene();
            }

            if (GUILayout.Button("Load"))
            {
                LoadScene();
            }
        }

        if (EditorSceneManager.sceneCount > 1)
        {
            if (GUILayout.Button("unload"))
            {
                unloadscene();
            }
        }
    }

    private void unloadscene()
    {
        for (int i = 0; i < EditorSceneManager.sceneCount; i++)
        {
            if (SceneManager.GetActiveScene() != SceneManager.GetSceneAt(i))
            {
                EditorSceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i));
            }
        }
    }


    private void CreateScene()
    {
        if (!Directory.Exists("Assets/Scenes/" + scenename))
        {
            Directory.CreateDirectory("Assets/Scenes/" + scenename);
            SetupScene();
            AssetDatabase.Refresh();
            GUI.FocusControl("");
            LoadScene();
            scenename = "";
            GUI.FocusControl("");
        }
    }

    private void SetupScene()
    {
        foreach (var term in terminations)
        {
            var logicScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            string temporal = term.Replace("_", "").Replace(".unity", "");

            new GameObject(temporal);


            EditorSceneManager.SaveScene(logicScene, "Assets/Scenes/" + scenename + "/" + scenename + term);
        }
    }

    private void LoadScene()
    {
        if (scenename.Length != 0)
        {
            EditorSceneManager.OpenScene("Assets/Scenes/develop.unity", OpenSceneMode.Single);

            foreach (var term in terminations)
            {
                EditorSceneManager.OpenScene("Assets/Scenes/" + scenename + "/" + scenename + term,
                    OpenSceneMode.Additive);
            }
        }

        AdditiveScenesControl loadAdditiveSceneManager = GameObject.FindObjectOfType<AdditiveScenesControl>();

        loadAdditiveSceneManager.CurrentLevel = scenename;
        EditorUtility.SetDirty(loadAdditiveSceneManager);
        scenename = "";
        GUI.FocusControl("");
    }
}