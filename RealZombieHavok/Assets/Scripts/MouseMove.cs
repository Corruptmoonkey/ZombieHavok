using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour
{
    public float mouseSensitivity = 500f;

    float xRotation = 0f;
    float yRotation = 0f;

    public float topClamp = -90f;
    public float bottomClamp = 90f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {


        // Getting Mosue Inputs
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        // Look up and down
        xRotation -= mouseY;


        // Clamp  rotation
        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);
       

        // Look left and right
        yRotation += mouseX;
       

        // Apply rotations to the transform
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
