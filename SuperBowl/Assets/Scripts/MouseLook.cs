using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Handles the player's roation on the basic mouse input
public class MouseLook : MonoBehaviour
{
    public float lookSensitivity = 2f, lookSmoothDamp = .5f;
    [HideInInspector]
    public float yRot , xRot;
    [HideInInspector]
    public float currentY, currentX;
    [HideInInspector]
    public float yRotationV, xRotationV;
    // Start is called before the first frame update
    /*void Start()
    {
        
    }*/

    // Update is called once per frame
    void LateUpdate()
    {
        yRot += Input.GetAxis("Mouse X") * lookSensitivity; //reads values from mouse Axis
        xRot += Input.GetAxis("Mouse Y") * lookSensitivity;

        currentX = Mathf.SmoothDamp(currentX, xRot, ref xRotationV, lookSmoothDamp); //moves float value from current to desired value over time
        currentY = Mathf.SmoothDamp(currentY,yRot, ref yRotationV, lookSmoothDamp);

        xRot = Mathf.SmoothDamp(xRot, -80, 80); // restricts xRotation value to be in between -80 and 80, prevents backflip
        transform.rotation = Quaternion.Euler(-currentX, currentY, 0); //setting rotation of camera according to mouse input
    }
}
