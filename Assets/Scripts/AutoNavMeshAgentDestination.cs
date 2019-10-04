using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AutoNavMeshAgentDestination : MonoBehaviour
{
    public Transform destination;
 
    private NavMeshAgent navMeshAgent;
 
    void Start ()
    {
        this.navMeshAgent = gameObject.GetComponent<NavMeshAgent>() as NavMeshAgent;

        if (this.navMeshAgent == null) {
            Debug.LogError("No NavMeshAgent assigned", this);
            return;
        }

        NavMeshHit closestHit;
        if (this.navMeshAgent.enabled == false && 
            NavMesh.SamplePosition(this.transform.position, out closestHit, 100f, NavMesh.AllAreas)) {
            this.transform.position = closestHit.position;
            this.navMeshAgent.enabled = true;
        }

        if (this.destination == null) {
            Debug.LogError("No destination assigned", this);
            return;
        }

        if (this.navMeshAgent.SetDestination(this.destination.position)) {
            Debug.Log("Destination set to NavMeshAgent", this);
        } else {
            Debug.LogError("Destination not set to NavMeshAgent", this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
