using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Diagnostics.Contracts;

public class SpawnedNavMeshAgentNavigator : MonoBehaviour {
    private NavMeshAgent cachedNavMeshAgent;
    NavMeshAgent GetNavMeshAgent(bool allowSeekNavMesh = true) {
        if (this.cachedNavMeshAgent != null) {
            return this.cachedNavMeshAgent;
        }

        NavMeshAgent result = this.GetComponent<NavMeshAgent>() as NavMeshAgent;
        if (result != null) {
            this.cachedNavMeshAgent = result;
        } else  {
            result = this.gameObject.AddComponent<NavMeshAgent>();
            this.cachedNavMeshAgent = result;
        }
        if (!result.isOnNavMesh && allowSeekNavMesh) {
            NavMeshHit closestHit;
            if (NavMesh.SamplePosition(this.transform.position, out closestHit, 500, 1)) {
                Debug.Log("Manually moving Spawned " + closestHit.distance + " units to NavMesh");
                //this.gameObject.transform.position = closestHit.position;

                result.Warp(closestHit.position);
                result.enabled = true;

            } else {
                Debug.LogError("Unable to seek to NavMesh", this);
            }
        }
        return result;
    }
    public NavMeshAgent EnsureNavMeshAgent() {
        return this.GetNavMeshAgent(true);
    }

    private Transform destination;

    void Start() {
        //if (this.navMeshAgent.enabled == false &&
        //    NavMesh.SamplePosition(this.transform.position, out NavMeshHit closestHit, 100f, NavMesh.AllAreas)) {
        //    this.transform.position = closestHit.position;
        //    this.navMeshAgent.enabled = true;
        //}
    }

    void Update() {
    }

    public void SetDestination(Transform transform) {
        Contract.Requires(transform != null);

        NavMeshAgent navMeshAgent = this.GetNavMeshAgent(false);
        // TODO: throw error
        if (navMeshAgent == null) {
            Debug.LogError("No NavMeshAgent assigned", this);
            return;
        }

        this.destination = transform;
        if (this.destination == null) {
            Debug.LogError("Destination cannot be null", this);
            return;
        }


        //NavMeshHit closestHit;
        //if (this.navMeshAgent.enabled == false &&
        //    NavMesh.SamplePosition(this.transform.position, out closestHit, 100f, NavMesh.AllAreas)) {
        //    this.transform.position = closestHit.position;
        //    this.navMeshAgent.enabled = true;
        //}

        if (navMeshAgent.SetDestination(this.destination.position)) {
            //Debug.Log("Destination set for NavMeshAgent", this);
        } else {
            Debug.LogError("Destination not set for NavMeshAgent", this);
        }
    }

    public void SetAreaCostOverrides(int[] costs) {
        if (costs == null || costs.Length == 0) {
            return;
        }

        NavMeshAgent navMeshAgent = this.GetNavMeshAgent(false);

        // TODO: throw error
        if (navMeshAgent == null) {
            Debug.LogError("No NavMeshAgent assigned", this);
            return;
        }

        for(int index = 0; index < costs.Length; index ++) {
            if (costs[index] <= 0) {
                continue;
            }

            navMeshAgent.SetAreaCost(index, costs[index]);
        }
    }

    public bool IsAtDestination() {
        Bounds bounds = new Bounds(this.destination.transform.position, new Vector3(10, 10, 10)); // TODO: size from Spawner
        return bounds.Contains(this.transform.position);
    }
}
