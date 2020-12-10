using UnityEngine;

public class npcProduction : MonoBehaviour
{
    [SerializeField] npcInventory npcInventory;
    [SerializeField] npcMovement npcMovement;
    [SerializeField] GameObject npcTray;
    [SerializeField] npcUI npcUI;

    public Animator npcAnimator;

    Animator printerAnimator;
    printerInfo printerInfo;
    Agater300 agater300;

    uint time;

    private void Awake() {
        npcInventory.NPC_PREPARING = true;
    }

    public bool PickItems() {
        printerInfo = npcMovement.FindRequestedObject("Printer").GetComponent<printerInfo>();
        npcUI.progressBar.SetActive(true);
        npcUI.maximum = printerInfo.MAX_CASES;

        time++;

        if (time == npcMovement.destination.GetComponent<shelfInfo>().PICK_SPEED) {
            npcInventory.NPC_CASES++;
            npcUI.current = npcInventory.NPC_CASES;

            if (npcInventory.NPC_CASES == printerInfo.MAX_CASES) {
                time = 0;
                npcUI.progressBar.SetActive(false);
                return true;
            }
            time = 0;
        }
        return false;
    }

    public bool Prepare() {
        npcUI.maximum = printerInfo.MAX_CASES;

        if (npcInventory.NPC_PREPARING) {
            npcTray.SetActive(true);
            npcUI.progressBar.SetActive(true);
            npcAnimator.Play("PreparingTrayAgater300");
            npcInventory.NPC_PREPARING = false;
        }

        time++;

        if (time == 50) {
            npcInventory.NPC_PREPARED_CASES++;
            npcInventory.NPC_CASES--;
            npcUI.current = npcInventory.NPC_PREPARED_CASES;

            if (npcInventory.NPC_CASES == 0) {
                npcInventory.NPC_PREPARED_CASES = 0;
                npcInventory.NPC_TRAY = true;
                npcUI.progressBar.SetActive(false);
                time = 0;

                return true;
            }
            time = 0;
        }
        return false;
    }

    public bool Printer() {
        printerAnimator = npcMovement.destination.GetComponent<Animator>();
        printerInfo = npcMovement.destination.GetComponent<printerInfo>();
        agater300 = npcMovement.destination.GetComponent<Agater300>();
        npcUI.progressBar.SetActive(true);
        npcUI.maximum = printerInfo.START_TIME;

        switch (printerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {

            case true:
                time++;
                npcUI.current = time;


                if (time == printerInfo.START_TIME) {
                    npcTray.SetActive(false);
                    npcUI.progressBar.SetActive(false);

                    agater300.Print();
                    npcInventory.NPC_TRAY = false;
                    time = 0;

                    return true;
                }
                break;

            case false:
                npcMovement.ChangeObject();
                break;
        }
        return false;
    }
}
