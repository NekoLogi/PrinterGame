using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject[] findObject;
    GameObject destination;
    public Text npcStatus;

    string[] objects;
    int time;
    int cases;
    int prepCases;
    int step; // 1 = picking | 2 = Preparing | 3 = Placing Tray
    float prevDistance;
    bool tray;

    void Start() {
        step = 3;
        prevDistance = 9999999999999;
        objects = new string[] { "Shelf", "PrepareTable", "Printer" };

        agent = gameObject.GetComponent<NavMeshAgent>();
        findObject = GameObject.FindGameObjectsWithTag(objects[0]);

        FindObject(0);

        //npcStatus = gameObject.GetComponentInChildren<GameObject>().GetComponentInChildren<Canvas>().GetComponentInChildren<Text>();
    }

    void Update() {

    }

    void FixedUpdate() {
        try {
        agent.SetDestination(destination.GetComponent<SphereCollider>().bounds.center);
        } catch (System.Exception) { }

        if (step == 0) {
            PickItems();
        }
        if (step == 1) {
            Prepare();
        }
        if (step == 2) {
            if (tray == true) {
            Place();
            } else {
                FindObject(0);
            }
        }
    }

    #region Methodes

    void OnTriggerEnter(Collider other) {
        switch (destination.tag) {

            case "Shelf":
                Debug.Log("Shelf");
                npcStatus.text = "Picking Cases";
                time = 0;
                cases = 0;
                step = 0;
                break;

            case "PrepareTable":
                Debug.Log("Prepare Table");
                npcStatus.text = "Prepare Tray";
                time = 0;
                step = 1;
                break;

            case "Printer":
                Debug.Log("Printer");
                npcStatus.text = "Placing Tray";
                time = 0;
                step = 2;
                break;
        }
    }

    void FindObject(int i) {
        //Go to nearest object and pick the cases.
        findObject = GameObject.FindGameObjectsWithTag(objects[i]);

        foreach (var productionObject in findObject) {
            float distance = Vector3.Distance(productionObject.transform.position, gameObject.transform.position);
            if (distance > prevDistance) {
                prevDistance = distance;
            } else {
                destination = productionObject;
            }
        }
    }

    void PickItems() {
        time++;
        if (time == 100) {
            cases++;
            try {
                npcStatus.text = $"Picking Cases: {cases}";
            } catch (System.Exception) { }

            if (cases == 4) {
                time = 0;
                step = 3;
                npcStatus.text = "";

                FindObject(1);
            }
            time = 0;
        }
    }

    void Prepare() {
        time++;
        if (time == 500) {
            prepCases++;
            try {
                npcStatus.text = $"Prepare Tray: {prepCases}";
            } catch (System.Exception) { }

            cases--;
            if (cases == 0) {
                tray = true;
                prepCases = 0;
                step = 3;
                npcStatus.text = "";

                FindObject(2);
            }
            time = 0;
        }
    }

    void Place() {
        time++;
        if (time == 100) {

            if (destination.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
                step = 3;
                npcStatus.text = "";

                StartPrinter();

                tray = false;
            }

            time = 0;
        }
    }

    void StartPrinter() {
        npcStatus.text = "Starting Printer";
        if (time == 100) {
            destination.GetComponent<Agater300>().Print();
            npcStatus.text = "";
            step = 3;
            time = 0;

            FindObject(0);

        }
    }

    #endregion
}
