using UnityEngine;
using UnityEngine.UI;

public class npcUI : MonoBehaviour
{
    [SerializeField] Text npcStatus;
    [SerializeField] Image mask;
    public GameObject progressBar;

    public uint maximum { get; set; }
    public uint current { get; set; }

    public void UIController() {
        SetPosition();

        if (progressBar.activeSelf) {
            GetCurrentFill();

        } else if (current != 0) {
            current = 0;
        }
    }

    void SetPosition() {
        Vector3 statusPos = Camera.main.WorldToScreenPoint(transform.position);
        npcStatus.transform.position = statusPos;
    }

    void GetCurrentFill() {
        float result = (float)current / (float)maximum;
        mask.fillAmount = result;
    }
}