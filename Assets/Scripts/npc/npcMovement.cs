using UnityEngine;
using UnityEngine.AI;

public class npcMovement : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] npcRole npcRole;

    public GameObject destination;

    GameObject[] Object;

    string newDestination;
    float prevDistance;
    int nextObject;

    void Awake() {
        prevDistance = 9999999999999;
    }

    public void SetDirection() {
        if (destination.GetComponent<CheckCollider>().inUse) {
            if (CheckInUse()) {
                agent.isStopped = false;
                agent.SetDestination(destination.GetComponent<SphereCollider>().bounds.center);

                if (destination.tag != (newDestination = npcRole.SetRole())) {
                    FindObject(newDestination);
                }

            } else {
                agent.isStopped = true;
                ChangeObject();
            }
        } else {
            agent.isStopped = false;
            agent.SetDestination(destination.GetComponent<SphereCollider>().bounds.center);
        }
    }

    public void FindObject(string tag) {
        Object = GameObject.FindGameObjectsWithTag(tag);
        nextObject = Object.Length - 1;

        foreach (var productionObject in Object) {
            float distance = Vector3.Distance(productionObject.transform.position, gameObject.transform.position);
            if (distance > prevDistance) {
                prevDistance = distance;
            } else {
                destination = productionObject;
            }
        }
    }

    public GameObject FindRequestedObject(string tag) {
        Object = GameObject.FindGameObjectsWithTag(tag);
        nextObject = Object.Length - 1;

        foreach (var productionObject in Object) {
            float distance = Vector3.Distance(productionObject.transform.position, gameObject.transform.position);
            if (distance > prevDistance) {
                prevDistance = distance;
            } else {
                return productionObject;
            }
        }
        return null;
    }

    public bool CheckInUse() {
        if (destination.GetComponent<SphereCollider>().bounds.Intersects(gameObject.GetComponent<Collider>().bounds)) {
            return true;
        } else {
            return false;
        }
    }

    void ChangeObject() {
        if (Object.Length > 1) {
            if (nextObject != 0) {
                nextObject--;
                destination = Object[nextObject];
            } else {
                //FindObject(step);
            }
        }
    }
}
