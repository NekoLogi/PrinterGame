using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGrid : MonoBehaviour
{
    [SerializeField] float gridSize;

    private GameObject structure = null;
    private Vector3 truePos;
    private bool selectObject;

    void Update() {
        //Change select mode to true/false.
        if (Input.GetMouseButtonDown(0)) {
            GetMouseWorldPos();
            if (structure != null) {
                ToggleObject();
            }
        }
    }

    void LateUpdate() {
        if (selectObject == true) {
            //Calculate grid position of current mouse position.
            truePos.x = Mathf.Floor(GetMouseWorldPos().x / gridSize) * gridSize;
            truePos.y = Mathf.Floor(GetMouseWorldPos().y / gridSize) * gridSize;
            truePos.z = Mathf.Floor(GetMouseWorldPos().z / gridSize) * gridSize;

            //Move object if select mode is true.
            if (selectObject == true && structure != null) {
                structure.transform.position = truePos;
            }
            if (Input.GetKeyDown(KeyCode.R)) {
                structure.transform.Rotate(0f, 90f, 0f);
            }
        }
    }

    #region Methodes

    private Vector3 GetMouseWorldPos() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;

        //If ray hit is in distance and of tag "Selectable", set gameobject info into variable.
        if (Physics.Raycast(ray, out hitData, 1000)) {
            if (hitData.transform.gameObject.CompareTag("Selectable")) {
                structure = hitData.transform.gameObject;
            }
        }
        return new Vector3(hitData.point.x, hitData.point.y, hitData.point.z);
    }

    private void ToggleObject() {
        switch (selectObject) {
            case true:
                selectObject = false;
                structure.GetComponentInChildren<Collider>().enabled = true;
                structure = null;
                break;
            case false:
                selectObject = true;
                structure.GetComponentInChildren<Collider>().enabled = false;
                break;
        }
    }

    #endregion
}
