using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    public float Sensitivity = 350f;
    float XRotate = 0f;
    float YRotate = 0f; 
    public float UpperClamp = -90f;
    public float LowerClamp = 90f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * Sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * Sensitivity * Time.deltaTime;
        
        XRotate -= mouseY;
        XRotate = Mathf.Clamp(XRotate, UpperClamp, LowerClamp);
        YRotate += mouseX;
        transform.localRotation = Quaternion.Euler(XRotate, YRotate, 0f);
    }
}
