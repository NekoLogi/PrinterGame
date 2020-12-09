using UnityEngine;

public class shelfInfo : MonoBehaviour
{
    public string SHELF_NAME { get; set; }
    public uint STOCK_CASES { get; set; }
    public uint PICK_SPEED { get; set; }

    private void Awake() {
        PICK_SPEED = 50;
        STOCK_CASES = 50;
    }
}
