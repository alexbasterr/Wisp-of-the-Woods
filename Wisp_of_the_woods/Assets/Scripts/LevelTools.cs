using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelTools : MonoBehaviour
{
    [Header("TAG")]
    public string childTag = "Untagged";

    [Header("SNAP")]
    public LayerMask groundLayers;

    [Header("REPLACE")]
    public GameObject replaceObject;

    [Header("ROTATE")]
    public float minYRotation = 0f;
    public float maxYRotation = 360f;

    public float minZRotation = -5f;
    public float maxZRotation = 5;

    public float minXRotation = -5f;
    public float maxXRotation = 5;

    [Header("SCALE")]
    public float minScale = 0.65f;
    public float maxScale = 0.8f;

    public void Scale()
    {
        foreach (GameObject child in GetChildsWithTag(childTag))
        {
            float scaleFactor = Random.Range(minScale, maxScale);
            child.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
        }
    }

    public void Rotate()
    {
        foreach (GameObject child in GetChildsWithTag(childTag))
        {
            child.transform.eulerAngles = new Vector3(Random.Range(minXRotation, maxXRotation), Random.Range(minYRotation, maxYRotation), Random.Range(minZRotation, maxZRotation));
        }
    }

    public void Replace()
    {
        foreach (GameObject child in GetChildsWithTag(childTag))
        {
            GameObject newGO = Instantiate(replaceObject, child.transform.position, child.transform.rotation);
            child.SetActive(false);
        }
    }

    public void Snap()
    {
        foreach(GameObject child in GetChildsWithTag(childTag))
        {
            SnapObject(child.transform);
        }
    }

    void SnapObject(Transform child)
    {
        Ray ray = new Ray();
        ray.origin = child.transform.position;

        ray.direction = -Vector3.up;

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit,Mathf.Infinity,groundLayers))
        {
            child.position = hit.point-new Vector3(0,0.1f,0);
        }
    }

    List<GameObject> GetChildsWithTag(string _tag)
    {
        List<GameObject> childsWithTag = new List<GameObject>();
        Transform[] childs = GetComponentsInChildren<Transform>();
        foreach (Transform child in childs)
        {
            if (child.gameObject.CompareTag(childTag))
            {
                childsWithTag.Add(child.gameObject);
            }
        }
        return childsWithTag;
    }


}
