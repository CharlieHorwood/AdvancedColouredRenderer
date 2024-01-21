using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using RTSEngine.Health;
using RTSEngine.Entities;
using RTSEngine.Utilities;

[CustomEditor(typeof(EntityColouredObjectsLoader))]

public class EntityColouredObjectsLoaderEditor : Editor
{

    private EntityColouredObjectsLoader Instance = null;

    SerializedProperty IsRendererLoaded;
    SerializedProperty coloredRenderers;
    SerializedProperty UseParentNotList;
    SerializedProperty ObjectsParent;
    SerializedProperty ObjectsToColour;
    SerializedProperty MaterialPropertyNames;

    void OnEnable()
    {
        this.Instance = (EntityColouredObjectsLoader)target;
        IsRendererLoaded = serializedObject.FindProperty("IsRendererLoaded");
        coloredRenderers = serializedObject.FindProperty("coloredRenderers");
        UseParentNotList = serializedObject.FindProperty("UseParentNotList");
        ObjectsParent = serializedObject.FindProperty("ObjectsParent");
        ObjectsToColour = serializedObject.FindProperty("ObjectsToColour");
        MaterialPropertyNames = serializedObject.FindProperty("MaterialPropertyNames");

    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        string[] topRow = {
            "Configuration",
            "Debug"
        };
        Instance.TabID = GUILayout.SelectionGrid(Instance.TabID, topRow, 2);
        switch (Instance.TabID)
        {
            case 0:
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.PropertyField(MaterialPropertyNames);
                EditorGUILayout.PropertyField(UseParentNotList);
                if (Instance.UseParentNotList)
                {
                    EditorGUILayout.PropertyField(ObjectsParent);
                }
                else
                {
                    EditorGUILayout.PropertyField(ObjectsToColour);
                }
                if (Instance != null)
                {
                    if (Instance.UseParentNotList && Instance.ObjectsParent != null)
                    {
                        if (GUILayout.Button("Load renderers from parent"))
                        {
                            LoadRenderersFromParent();
                        }
                    }
                    else if(!Instance.UseParentNotList && Instance.ObjectsToColour.Count > 0)
                    {
                        if (GUILayout.Button("Load renderers from list"))
                        {
                            LoadRenderersFromList();
                        }
                    }
                    EditorGUILayout.PropertyField(coloredRenderers);
                }
                EditorGUILayout.EndVertical();
                break;
            case 1:
                EditorGUILayout.BeginVertical("box");
                
                EditorGUILayout.EndVertical();
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }

    public void LoadRenderersFromParent()
    {
        if(Instance.ObjectsParent.childCount > 0)
        {
            List<MeshRenderer> RenderersList = new();
            foreach(Transform child in Instance.ObjectsParent)
            {
                if (child != null)
                {
                    if (child.TryGetComponent(out MeshRenderer meshRenderer))
                    {
                        RenderersList.Add(meshRenderer);
                    }
                }
            }
            if(RenderersList.Count > 0)
            {
                Instance.SetRenderers(RenderersList);
            }
        }
    }
    public void LoadRenderersFromList()
    {
        if (Instance.ObjectsToColour.Count > 0)
        {
            List<MeshRenderer> RenderersList = new();
            foreach (MeshRenderer child in Instance.ObjectsParent)
            {
                if (child != null)
                {
                    RenderersList.Add(child);
                }
            }

            if (RenderersList.Count > 0)
            {
                Instance.SetRenderers(RenderersList);
            }
        }
    }

}
