using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ScientistCheckpointManager))]
public class ScientistCheckpointManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ScientistCheckpointManager tar = (ScientistCheckpointManager)target;

        if (GUILayout.Button("Show Checkpoints"))
        {
            tar.ShowCheckpoints();
        }

        if (GUILayout.Button("Hide Checkpoints"))
        {
            tar.HideCheckpoints();
        }



    }
}
