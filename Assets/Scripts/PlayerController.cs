using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    GameObject[] findObject;
    GameObject destination;
    public NavMeshAgent agent;
    public Image progressBar;
    public GameObject preparedTray;
    public Animator animator;
    public Text npcStatus;

    string[] objects;
    int time;
    int cases;
    int prepCases;
    int nextObject;
    int step; // 0 = picking | 1 = Preparing | 2 = Placing Tray
    float prevDistance;
    bool tray;
    bool preparingTray;

    void Start() {
        step = 3;
        prevDistance = 9999999999999;
        objects = new string[] { "Shelf", "PrepareTable", "Printer" };

        findObject = GameObject.FindGameObjectsWithTag(objects[0]);

        progressBar.gameObject.SetActive(false);

        FindObject(0);

        SetDestination();
    }
    

    void FixedUpdate() {
        SetDestination();

        switch (step) {
            case 0:
                PickItems();
                break;

            case 1:
                Prepare();
                break;

            case 2:
                if (tray == true) {
                    Printer();
                } else {
                    FindObject(0);
                }
                break;
        }
    }

    void OnTriggerEnter(Collider other) {
        switch (destination.tag) {

            case "Shelf":
                Debug.Log("Shelf");
                progressBar.GetComponent<ProgressBar>().current = 0;

                time = 0;
                cases = 0;
                step = 0;
                break;

            case "PrepareTable":
                progressBar.GetComponent<ProgressBar>().current = 0;
                time = 0;
                step = 1;
                preparingTray = true;
                break;

            case "Printer":
                time = 0;
                step = 2;
                break;
        }
    }

    ///////////////////////////////////


    #region Methodes

    void SetDestination() {
        try {
            if (destination.GetComponent<CheckCollider>().inUse) {
                if (CheckInUse()) {
                    agent.isStopped = false;
                    agent.SetDestination(destination.GetComponent<SphereCollider>().bounds.center);
                } else {
                    agent.isStopped = true;
                    ChangeObject();
                }
            } else {
                agent.isStopped = false;
                agent.SetDestination(destination.GetComponent<SphereCollider>().bounds.center);
            }

        } catch (System.Exception) { }
    }

    void FindObject(int i) {
        //Go to nearest object and pick the cases.
        findObject = GameObject.FindGameObjectsWithTag(objects[i]);
        nextObject = findObject.Length - 1;

        foreach (var productionObject in findObject) {
            float distance = Vector3.Distance(productionObject.transform.position, gameObject.transform.position);
            if (distance > prevDistance) {
                prevDistance = distance;
            } else {
                destination = productionObject;
            }
        }
    }

    void ChangeObject() {
        if (objects.Length > 1) {
            if (nextObject != 0) {
                nextObject--;
                destination = findObject[nextObject];
            } else {
                FindObject(step);
            }
        }
    }

    void PickItems() {
        switch (destination.GetComponent<CheckCollider>().inUse && CheckInUse()) {
            case true:
                progressBar.gameObject.SetActive(true);
                progressBar.GetComponent<ProgressBar>().maximum = 4;
                time++;

                if (time == 50) {
                    cases++;

                    progressBar.GetComponent<ProgressBar>().current = cases;
                    try {
                    } catch (System.Exception) { }

                    if (cases == 4) {
                        time = 0;
                        step = 3;
                        progressBar.gameObject.SetActive(false);
                        agent.isStopped = false;

                        FindObject(1);
                    }
                    time = 0;
                }
                break;

            case false:
                ChangeObject();
                break;
        }
    }

    bool CheckInUse() {
        if (destination.GetComponent<SphereCollider>().bounds.Intersects(gameObject.GetComponent<Collider>().bounds)) {
            return true;
        } else {
            return false;
        }
    }

    void Prepare() {
        if (destination.GetComponent<CheckCollider>().inUse && CheckInUse()) {
        } else {
        }

        switch (destination.GetComponent<CheckCollider>().inUse && CheckInUse()) {
            case true:
                if (preparingTray == true) {
                    preparedTray.SetActive(true);
                    animator.Play("PreparingTrayAgater300");
                    preparingTray = false;
                }
                progressBar.gameObject.SetActive(true);
                progressBar.GetComponent<ProgressBar>().maximum = 4;
                preparedTray.SetActive(true);
                time++;
                if (time == 50) {
                    prepCases++;

                    progressBar.GetComponent<ProgressBar>().current = prepCases;
                    try {
                    } catch (System.Exception) { }

                    cases--;
                    if (cases == 0) {
                        tray = true;
                        prepCases = 0;
                        step = 3;
                        progressBar.gameObject.SetActive(false);
                        agent.isStopped = false;

                        FindObject(2);
                    }
                    time = 0;
                }
                break;

            case false:
                ChangeObject();
                break;
        }

    }

    void Printer() {
        switch (destination.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
            case true:
                if (gameObject.GetComponent<Collider>().bounds.Intersects(destination.GetComponent<SphereCollider>().bounds)) {
                    progressBar.gameObject.SetActive(true);
                    progressBar.GetComponent<ProgressBar>().maximum = 100;
                    progressBar.GetComponent<ProgressBar>().current = time;

                    time++;
                    if (time == 100) {
                        progressBar.gameObject.SetActive(false);
                        preparedTray.SetActive(false);
                        agent.isStopped = false;

                        StartPrinter();
                    }
                }
                break;

            case false:
                if (objects.Length > 1) {
                    if (nextObject != 0) {
                        nextObject--;
                        destination = findObject[nextObject];
                    } else {
                        FindObject(2);
                    }
                }
                break;
        }
    }

    void StartPrinter() {
            destination.GetComponent<Agater300>().Print();
            tray = false;
            step = 3;

            FindObject(0);
    }

    #endregion
}
