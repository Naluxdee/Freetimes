using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform cameraTransform;

    void Update()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }

        transform.LookAt(cameraTransform);
        transform.Rotate(0, 180, 0); // ให้หันด้านหน้า
    }
}

