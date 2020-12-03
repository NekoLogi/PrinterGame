using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    public int maximum;
    public int current;
    public Image mask;

    void FixedUpdate() {
        GetCurrentFill();
    }

    public void GetCurrentFill() {
        float result = (float)current / (float)maximum;
        mask.fillAmount = result;
    }
}
