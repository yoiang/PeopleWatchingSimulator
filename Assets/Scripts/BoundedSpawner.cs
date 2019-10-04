using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundedSpawner : Spawner
{
    public BoxCollider bounds;

    protected override GameObject SpawnInternal(Spawnee spawnee) {
        (GameObject, Spawned) result = spawnee.SpawnNewInstance(this.transform, this);
        if (result.Item1 != null) {

            SpawnedVelocityNavigator navigator = result.Item1.AddComponent<SpawnedVelocityNavigator>();
            if (navigator != null) {
                navigator.velocity = Utility.RandomWithin(spawnee.speed);
                return result.Item1;
            }

            Destroy(result.Item1);
        }

        return null;
    }

    protected override (Predicate<GameObject>, string) GetRemoveAsNeededInfo() {
        return (
            (obj) => {
                return !(this.bounds.bounds.Contains(obj.gameObject.transform.position));
            },
            "out of bounds"
        );
    }
}
