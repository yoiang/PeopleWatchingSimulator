using UnityEngine;
using System.Collections;

public class PetBowl : MonoBehaviour, IUsable {
    public GameObject petPlaceholder;

    public GameObject[] pets;
    private int petIndex = 0;
    private GameObject currentPet;

    void Start() {
        this.currentPet = this.petPlaceholder;
        this.petPlaceholder = null;
    }

    // Update is called once per frame
    void Update() {
    }

    public void Use() {
        this.SwapPet();
    }

    void SwapPet() {
        Vector3 position;
        Quaternion rotation;
        if (this.currentPet != null) {
            position = this.currentPet.transform.position;
            rotation = this.currentPet.transform.rotation;

            Object.Destroy(this.currentPet);
            this.currentPet = null;
        } else {
            Debug.LogError("No previous pet assigned", this);
            return;
        }

        if (this.pets.Length == 0) {
            Debug.LogError("No pets assigned", this);
            return;
        }

        if (this.petIndex < 0 || this.petIndex >= this.pets.Length) {
            this.petIndex = 0;
        }

        // TODO: play sound, ca-ching?
        this.currentPet = Object.Instantiate(
            this.pets[petIndex], 
            position, 
            rotation);
        this.petIndex++;
        this.currentPet.SetActive(true);

        Debug.Log("Swapped pet to " + this.currentPet, this);
    }
}
