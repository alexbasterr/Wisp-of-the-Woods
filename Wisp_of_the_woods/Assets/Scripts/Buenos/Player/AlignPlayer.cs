using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignPlayer : MonoBehaviour
{

    private void Update()
    {
        AlignToFloor();
    }

    void AlignToFloor()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 10))
        {
            if(hit.transform.gameObject.tag == "Suelo")
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation, Time.deltaTime * 5);
                transform.position = hit.point + transform.up * 0.17f;
            }
        }
    }

    /*
    Vector3 CalculateMidPoint(Vector3 point1, Vector3 point2)
    {
        Vector3 solution;

        solution = new Vector3(point1.x + (point2.x - point1.x) / 2, point1.y + (point2.y - point1.y) / 2, point1.y + (point2.y - point1.y) / 2);

        return solution;
    }
    */
}
