using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour, IUsable
{
    public GameObject plate;

    public GameObject[] food;
    private int foodIndex = 0;
    private GameObject currentFood;

    void Start()
    {
        // Don't automatically spawn food, teach them they need to use the menu
        //this.SpawnFood();
    }

    void Update()
    {
        
    }

    private void SpawnFood() {
        if (this.currentFood != null) {
            Destroy(this.currentFood);
            this.currentFood = null;
        }

        if (this.food.Length == 0) {
            Debug.LogError("No food assigned", this);
            return;
        }

        if (this.plate == null) {
            Debug.LogError("No plate assigned", this);
            return;
        }

        if (this.foodIndex < 0 || this.foodIndex >= this.food.Length) {
            this.foodIndex = 0;
        }

        // TODO: play sound, ca-ching?
        this.currentFood = Object.Instantiate(this.food[foodIndex], this.plate.transform.position + new Vector3(0, 1, 0), this.food[foodIndex].transform.rotation, this.plate.transform);//this.transform);
        this.foodIndex++;
        this.currentFood.SetActive(true);

        Debug.Log("Swapped food to " + this.currentFood, this);
    }

    public void Use() {
        this.SpawnFood();
    }
}
