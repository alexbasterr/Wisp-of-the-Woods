using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RayDirection
{
    Up,Down
}

public class SnapToGround : MonoBehaviour
{
    public string childTag = "SnapObject";
    public LayerMask groundLayers;
    public RayDirection direction = RayDirection.Down;
    
    public void Snap()
    {
        Transform[] childs = GetComponentsInChildren<Transform>();
        foreach(Transform child in childs)
        {
            if(child.gameObject.CompareTag(childTag))
            {
                SnapObject(child);
            }
        }
    }

    void SnapObject(Transform child)
    {
        Ray ray = new Ray();
        ray.origin = child.transform.position;
        
        if(direction==RayDirection.Up)
        {
            ray.direction = Vector3.up;
        }
        else if (direction==RayDirection.Down)
        {
            ray.direction = -Vector3.up;
        }
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit,Mathf.Infinity,groundLayers))
        {
            child.position = hit.point-new Vector3(0,0.1f,0);
        }
    }


}
