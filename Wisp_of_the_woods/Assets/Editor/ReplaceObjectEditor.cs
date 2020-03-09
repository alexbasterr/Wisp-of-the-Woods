using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ReplaceObject))]
public class ReplaceObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ReplaceObject snapToGround = (ReplaceObject)target;

        if (GUILayout.Button("Replace Objects"))
        {
            snapToGround.Replace();
        }

    }
}
