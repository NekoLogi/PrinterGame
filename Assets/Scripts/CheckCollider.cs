using UnityEngine;

public class CheckCollider : MonoBehaviour
{
    public bool inUse;

    void Awake() {
        inUse = false;
    }

    private void OnTriggerEnter(Collider other) {
        inUse = true;
    }

    private void OnTriggerExit(Collider other) {
        inUse = false;
    }
}
