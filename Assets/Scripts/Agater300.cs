using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agater300 : MonoBehaviour
{
    Animator animator;
    AudioSource printerAudio;

    void Start() {
        animator = GetComponent<Animator>();
        printerAudio = GetComponent<AudioSource>();
    }

    void Update() {
        if (Input.GetKey(KeyCode.C)) {
            Print();
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Agater 300 Tool Finished")) {
            Finish();
        }
    }

    #region Methodes

    public void Print() {
        if (!printerAudio.isPlaying) {
            animator.Play("Agater 300 Tool Printing");
            printerAudio.Play(0);
        }
    }

    void Finish() {
        printerAudio.Stop();
    }

    void Error() {
        printerAudio.Pause();
    }

    #endregion
}
