using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ScaleObject))]
public class ScaleObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ScaleObject snapToGround = (ScaleObject)target;

        if (GUILayout.Button("Scale Objects"))
        {
            snapToGround.Scale();
        }

    }
}