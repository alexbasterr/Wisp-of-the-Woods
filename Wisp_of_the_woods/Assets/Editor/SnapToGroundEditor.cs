using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SnapToGround))]
public class SnapToGroundEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SnapToGround snapToGround = (SnapToGround)target;

        if(GUILayout.Button("Snap Object"))
        {
            snapToGround.Snap();
        }

    }
}
