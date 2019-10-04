using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Diagnostics.Contracts;
using System;

public class Spawned : MonoBehaviour {
    public void SetTargetRenderersEnabled(bool enable) {
        Renderer[] renderers = this.GetComponents(typeof(Renderer)) as Renderer[];
        if (renderers == null) {
            // TODO: throw error?
            //Debug.LogWarning("Source does not have any renderer", this);
            return;
        }
        foreach (Renderer renderer in renderers) {
            renderer.enabled = enable;
        }
    }
}
