using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGrid : MonoBehaviour
{
    [SerializeField] float gridSize;

    GameObject structure = null;
    Vector3 truePos;
    bool selectObject;
    string[] objectTags;

    Ray ray;
    RaycastHit hitData;

    private void Start() {
        objectTags = new string[] { "Printer", "Shelf", "Decor" };
    }

    void Update() {
        //Change select mode to true/false.
        if (Input.GetMouseButtonDown(0)) {
            GetMouseWorldPos();
            if (structure != null && Physics.Raycast(ray, out hitData, 1000)) {
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
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //If ray hit is in range and is of tag in array, set gameobject info into variable.
        if (Physics.Raycast(ray, out hitData, 1000)) {
            foreach (var tag in objectTags) {
                if (hitData.transform.gameObject.CompareTag(tag) && selectObject == false) {
                    structure = hitData.transform.gameObject;
                    break;
                }
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
