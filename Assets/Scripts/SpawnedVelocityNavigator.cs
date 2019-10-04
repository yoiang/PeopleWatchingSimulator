using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Diagnostics.Contracts;

public class SpawnedVelocityNavigator : MonoBehaviour {
    public Vector3 velocity;

    void Update() {
        this.transform.Translate(this.velocity * Time.deltaTime);
    }
}
