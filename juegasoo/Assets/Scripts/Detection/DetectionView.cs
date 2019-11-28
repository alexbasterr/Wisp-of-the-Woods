using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionView : MonoBehaviour
{

    private DetectionManager detectionManager;
    

    private void Awake()
    {
        detectionManager = transform.parent.parent.gameObject.GetComponent<DetectionManager>();
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!other.GetComponent<characterMovement>().escondido)
                detectionManager.detectadoVisual = true;
        }
    }
}
