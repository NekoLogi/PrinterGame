using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraView : MonoBehaviour
{
    [SerializeField] Transform playerBody;
    [SerializeField] Rigidbody rb;
    [SerializeField] float lookSensitivity = 1000f;
    float yRotation = 0f;
    float xRotation = 0f;
    bool toggledMouse = true;

    void Update() {
        if (Input.anyKey) {
            OnKeyPress();
        }
        yRotation = Mathf.Clamp(yRotation, 20f, 50f);
        //rb.AddTorque(new Vector3(yRotation, xRotation, 0f));
        transform.localRotation = Quaternion.Euler(yRotation, xRotation, 0f);
        
    }

    void OnKeyPress() {
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            float mouseY = -2f * lookSensitivity * Time.deltaTime;
            yRotation -= mouseY;
            //rb.AddTorque(new Vector3(0f, -30f, 0f));

        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            float mouseY = 2f * lookSensitivity * Time.deltaTime;
            yRotation -= mouseY;
            //rb.AddTorque(new Vector3(0f, 30f, 0f));

        }
        if (Input.GetKeyDown(KeyCode.Q)) {
            float mouseX = -2f * lookSensitivity * Time.deltaTime;
            rb.AddTorque(new Vector3(0f, -45f, 0f));
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            float mouseX = 2f * lookSensitivity * Time.deltaTime;
            rb.AddTorque(new Vector3(0f, 45f, 0f));

        }
        if (Input.GetKeyDown(KeyCode.Tab)) {
            switch (toggledMouse) {
                case true:
                    Cursor.lockState = CursorLockMode.Locked;
                    toggledMouse = false;
                    break;
                case false:
                    Cursor.lockState = CursorLockMode.Confined;
                    toggledMouse = true;
                    break;
            }
        }
    }
}