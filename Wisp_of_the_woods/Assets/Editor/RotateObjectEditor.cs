using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RotateObjects))]
public class RotateObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        RotateObjects snapToGround = (RotateObjects)target;

        if (GUILayout.Button("Rotate Objects"))
        {
            snapToGround.Rotate();
        }

    }
}
