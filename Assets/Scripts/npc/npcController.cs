using System.Threading.Tasks;
using UnityEngine;

public class npcController : MonoBehaviour
{
    [SerializeField] npcRole npcRole;
    [SerializeField] npcMovement npcMovement;
    [SerializeField] npcUI npcUI;

    private void Start() {
        npcMovement.destination = gameObject;
        npcMovement.FindObject(npcRole.SetRole());
    }

    private void FixedUpdate() {
        npcMovement.SetDirection();

        npcUI.UIController();

    }

}
