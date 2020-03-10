using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelTools))]
public class LevelToolsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        LevelTools tools = (LevelTools)target;

        GUILayout.Space(10);
        if(GUILayout.Button("Snap Objects"))
        {
            tools.Snap();
        }
        GUILayout.Space(10);
        if (GUILayout.Button("Replace Objects"))
        {
            tools.Replace();
        }
        GUILayout.Space(10);
        if (GUILayout.Button("Rotate Objects"))
        {
            tools.Rotate();
        }
        GUILayout.Space(10);
        if (GUILayout.Button("Scale Objects"))
        {
            tools.Scale();
        }

    }
}
