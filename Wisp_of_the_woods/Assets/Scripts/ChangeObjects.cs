using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeObjects : MonoBehaviour
{
    public GameObject[] objects;
    public GameObject replaceObject;

    [ContextMenu("Change Objects")]
    public void Change()
    {
        foreach(GameObject go in objects)
        {
            GameObject newGO=Instantiate(replaceObject, go.transform.position, go.transform.rotation);
            
        }
    }
}
