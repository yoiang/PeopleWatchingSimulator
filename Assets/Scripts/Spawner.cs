using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

// TODO: split off NavMeshAgent stuff to subclass
public class Spawner : MonoBehaviour
{
    public Spawnee[] spawnees;
    public int MaximumCount = 1;

    public int[] areaCostOverrides;

    protected List<GameObject> spawned;

    void Start()
    {
        this.spawned = new List<GameObject>();

        this.HideSpawnees();
    }

    void HideSpawnees() {
        foreach (Spawnee spawnee in this.spawnees) {
            spawnee.SetTargetRenderersEnabled(false);
        }
    }

    void Update()
    {
        this.SpawnAsNeeded();
        this.RemoveAsNeeded();
    }

    private Spawnee GetRandomSpawnee() {
        if (this.spawnees == null || this.spawnees.Length == 0) {
            return new Spawnee();
        }
        int index = Mathf.RoundToInt(UnityEngine.Random.Range(0, this.spawnees.Length));
        return this.spawnees[index];
    }

    private List<Spawner> GetAllOtherSpawners() {
        return new List<Spawner>(UnityEngine.Object.FindObjectsOfType(typeof(Spawner)) as Spawner[])
            .FindAll((obj) => {
                return obj != this
                && obj as BoundedSpawner == null // TODO: Clean hack, subclass NavMeshSpawner, potentially also tag?
                ;
                }
            ); 
    }

    private void SpawnAsNeeded()
    {
        while (this.spawned.Count < this.MaximumCount)
        {
            Spawnee spawnee = this.GetRandomSpawnee();
            if (spawnee == null) {
                Debug.LogError("Unable to retrieve random Spawnee", this);
                return;
            }

            GameObject result = this.SpawnInternal(spawnee);
            if (result != null) {
                Debug.Log("Spawned new instance from " + spawnee, this); // of " + newInstance + "
                this.spawned.Add(result);
            } else {
                Debug.LogError("Unable to spawn new instance from " + spawnee, this);
                return;
            }
        } 
    }

    protected virtual GameObject SpawnInternal(Spawnee spawnee) {
        (GameObject, Spawned) result = spawnee.SpawnNewInstance(this.transform, this);
        if (result.Item1 != null) {
            SpawnedNavMeshAgentNavigator navigator = result.Item1.AddComponent<SpawnedNavMeshAgentNavigator>();
            if (navigator != null) {
                try {
                    navigator.EnsureNavMeshAgent();
                    navigator.SetAreaCostOverrides(this.areaCostOverrides);

                    List<Spawner> otherSpawners = this.GetAllOtherSpawners();
                    if (otherSpawners == null || otherSpawners.Count == 0) {
                        throw new UnityException("Unable to find other Spawners to use as destination");
                    }

                    Spawner otherSpawner = otherSpawners[UnityEngine.Random.Range(0, otherSpawners.Count)];

                    navigator.SetDestination(otherSpawner.transform);
                    return result.Item1;

                } catch (System.Exception e) {
                    Debug.LogError(e, this);
                }

            }

            Destroy(result.Item1);
        }

        return null;
    }

    private void RemoveAsNeeded() {
        (Predicate<GameObject>, string) removeInfo = this.GetRemoveAsNeededInfo();

        List<GameObject> remove = this.FindSpawnedThatMatch(removeInfo.Item1);

        this.RemoveSpawned(remove, removeInfo.Item2);
    }

    protected virtual (Predicate<GameObject>, string) GetRemoveAsNeededInfo() {
        return (
            (obj) => {
                SpawnedNavMeshAgentNavigator navigator = obj.GetComponent(typeof(SpawnedNavMeshAgentNavigator)) as SpawnedNavMeshAgentNavigator;
                if (navigator != null) {
                    return navigator.IsAtDestination();
                } else {
                    // TODO: what should we do here?
                    return true;
                }
            },
            "at destination"
        );
    }

    protected List<GameObject> FindSpawnedThatMatch(Predicate<GameObject> match) {
        if (this.spawned == null) {
            Debug.LogError("No spawnees assigned");
            return null;
        }
        if (this.spawned.Count == 0) {
            return null;
        }
        return this.spawned.FindAll(match);
    }

    private void RemoveSpawned(List<GameObject> removes, string context) {
        if (removes == null) {
            return;
        }

        foreach (GameObject remove in removes) {
            this.RemoveSpawned(remove, context);
        }
    }

    private void RemoveSpawned(GameObject spawned, string context) {
        Debug.Log("Removing " + context + " instance of " + spawned, this);

        this.spawned.Remove(spawned);
        spawned.SetActive(false);
        Destroy(spawned);

    }
}
