using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npcUI : MonoBehaviour
{
    Text npcStatus;

    void Awake() {
        npcStatus = gameObject.GetComponentInChildren<Text>();
    }

    void FixedUpdate() {
        Vector3 statusPos = Camera.main.WorldToScreenPoint(transform.position);
        npcStatus.transform.position = statusPos;
    }
}