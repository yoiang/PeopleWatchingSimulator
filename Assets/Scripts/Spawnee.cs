using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Spawnee 
{
    public Bounds speed;

    public GameObject target;

    public void SetTargetRenderersEnabled(bool enable) {
        if (this.target == null) {
            return;
        }

        Renderer[] renderers = this.target.GetComponentsInChildren(typeof(Renderer)) as Renderer[];
        if (renderers == null) {
            // TODO: throw error?
            //Debug.LogWarning("Source does not have any renderer", this.target);
            return;
        }
        foreach (Renderer renderer in renderers) {
            renderer.enabled = enable;
        }
    }

    public (GameObject, Spawned) SpawnNewInstance(Transform matchPositionAndRotation, Spawner context) {
        if (this.target == null) {
            return (null, null);
        }

        GameObject result = Object.Instantiate(this.target, matchPositionAndRotation.position, matchPositionAndRotation.rotation, matchPositionAndRotation);
        if (result == null) {
            Debug.LogError("Instantiate returned null", context.gameObject);
            return (null, null);
        }

        Spawned spawned = result.AddComponent(typeof(Spawned)) as Spawned;
        if (spawned != null) {
            spawned.SetTargetRenderersEnabled(true);

            result.SetActive(true);
        }

        return (result, spawned);
    }
}
