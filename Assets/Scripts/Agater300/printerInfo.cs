using UnityEngine;

public class printerInfo : MonoBehaviour
{
    public uint MAX_CASES { get; set; }
    public uint START_TIME { get; set; }

    private void Awake() {
        MAX_CASES = 4;
        START_TIME = 100;
    }
}
