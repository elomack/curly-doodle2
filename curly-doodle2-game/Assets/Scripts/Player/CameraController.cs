using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity;
    public Quaternion originalRotation;
    public Vector3 originalPosition;

    private Transform parentTransform;

    private void Start()
    {
        parentTransform = transform.parent;
        originalRotation = transform.localRotation;
        originalPosition = transform.localPosition;
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Rotate();
            return;
        }

        if (Input.GetMouseButton(0))
        {
            RotateCamera();
            return;
        }

        if (Input.GetMouseButtonUp(0))
        {
            transform.localRotation = originalRotation;
            
        }
    }

    private void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        parentTransform.Rotate(Vector3.up, mouseX);
        
    }

    private void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up, mouseX, Space.World);
    }
}
