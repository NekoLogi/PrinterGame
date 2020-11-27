using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agater300 : MonoBehaviour
{
    Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
    }

    void Update() {
        if (Input.GetKey(KeyCode.C)) {
            animator.SetBool("isPrinting", true);
        }
        if (!Input.GetKey(KeyCode.C)) {
            animator.SetBool("isPrinting", false);
        }
        Debug.Log("C " + animator.GetBool("isPrinting"));
    }

    void Printing() {

    }

    void Finished() {

    }

    void Error() {

    }
}
