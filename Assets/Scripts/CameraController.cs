using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody rb;
    Vector3 movement;

    void Start() {
        
    }

    public Vector3 worldPosition;

    void Update() {
    }

    void FixedUpdate() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");

            rb.AddForce(movement * speed);

    }


    #region methodes

    #endregion
}
