using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nighttimer : MonoBehaviour
{
    private Light cachedLight;
    private float originalIntensity;
    void Start()
    {
        this.cachedLight = this.GetComponent(typeof(Light)) as Light;
        if (this.cachedLight != null)
        {
            this.originalIntensity = this.cachedLight.intensity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.cachedLight != null)
        {
            if (this.transform.up.x > 0)
            { 
                this.cachedLight.intensity = 0.01f;
            }
            else
            {
                this.cachedLight.intensity = this.originalIntensity;
            }
        }
    }
}
