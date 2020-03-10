using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ScientistCheckpoints))]
public class ScientistEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ScientistCheckpoints tar = (ScientistCheckpoints)target;
        
        if(GUILayout.Button("Show Checkpoints"))
        {
            foreach (Transform t in tar.scientist.Checkpoints)
            {
                t.gameObject.GetComponent<MeshRenderer>().enabled = true;
            }
        }

        if (GUILayout.Button("Hide Checkpoints"))
        {
            foreach (Transform t in tar.scientist.Checkpoints)
            {
                t.gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
        }



    }
}
