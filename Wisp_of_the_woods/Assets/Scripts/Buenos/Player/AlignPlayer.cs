using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignPlayer : MonoBehaviour
{

    private void FixedUpdate()
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
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation, Time.fixedDeltaTime * 5);
                transform.position = hit.point + transform.up * 0.17f;
            }
        }
    }
}
