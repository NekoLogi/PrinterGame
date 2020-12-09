using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcRole : MonoBehaviour
{
    [SerializeField] npcMovement npcMovement;
    [SerializeField] npcProduction npcProduction;
    [SerializeField] npcInventory npcInventory;

    public uint steps;
    string station;

    private void Awake() {
        if (station == null) {
            station = "Agater300";
        }
    }

    public string SetRole() {
        switch (station) {
            case "Agater300":
                if (npcMovement.destination == gameObject) {
                    return "Shelf";
                } else {
                    return Agater300();
                }
        }
        return null;
    }

    private string Agater300() {
        switch (steps) {
            case 0:
                if (npcProduction.PickItems()) {
                    steps++;
                    return "PrepareTable";
                } else {
                    return "Shelf";
                }

            case 1:
                if (npcProduction.Prepare()) {
                    steps++;
                    npcInventory.NPC_PREPARING = true;
                    return "Printer";
                } else {
                    return "PrepareTable";
                }

            case 2:
                if (npcProduction.Printer()) {
                    steps = 0;
                    return "Shelf";
                } else {
                    return "Printer";
                }
        }
        return null;
    }



}
