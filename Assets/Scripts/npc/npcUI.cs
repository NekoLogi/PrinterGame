using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class npcUI : MonoBehaviour
{
    public Text npcStatus;
    public Image mask;

    public int maximum;
    public int current;


    //private void FixedUpdate() {
    //    Task.Run(SetPosition);
    //}

    void SetPosition() {
        Vector3 statusPos = Camera.main.WorldToScreenPoint(transform.position);
        npcStatus.transform.position = statusPos;
    }

    public void GetCurrentFill() {
        float result = (float)current / (float)maximum;
        mask.fillAmount = result;
    }
}