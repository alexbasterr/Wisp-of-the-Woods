using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceObject : MonoBehaviour
{
    public GameObject[] objects;
    public GameObject replaceObject;
    

    public void Replace()
    {
        foreach(GameObject go in objects)
        {
            GameObject newGO=Instantiate(replaceObject, go.transform.position, go.transform.rotation);
            go.SetActive(false);
            
        }
    }
}
