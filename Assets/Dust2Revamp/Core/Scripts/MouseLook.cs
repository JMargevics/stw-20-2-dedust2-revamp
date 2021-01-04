using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class MouseLook : NetworkedBehaviour
{
    public float mouseSensitivity = 100f;

    public GameObject cam;
    public Transform playerBody;

    float xRotation = 0f;

    void Start()
    {
        if(IsLocalPlayer)
            Cursor.lockState = CursorLockMode.Locked;
        else
        {
            cam.SetActive(false);
        }
    }

    void Update()
    {
        if (IsLocalPlayer)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
