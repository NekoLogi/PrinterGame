using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;

    void LateUpdate() {
        Task.Run(GetCoordinates);
    }

    void GetCoordinates() {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    void OnMouseDrag()
    {
        transform.position = new Vector3(transform.position.x - mOffset.x, transform.position.y - mOffset.y + 1.5f, GetMouseWorldPos().z + mOffset.z);
    }

    private Vector3 GetMouseWorldPos() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;

        if (Physics.Raycast(ray, out hitData, 1000)) {
            Debug.Log(@"x = " + hitData.point.x + " y = " + hitData.point.y + " z = " + hitData.point.z);
        }

        return new Vector3(hitData.point.x, hitData.point.y, hitData.point.z);
    }
}
