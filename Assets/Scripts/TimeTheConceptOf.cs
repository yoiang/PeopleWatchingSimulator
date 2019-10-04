using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class TimeTheConceptOf : MonoBehaviour
{
    //public bool updateInEditMode;
    //static private Timer editorUpdateTimer;

    public float skyboxRotationRate;
    private float skyboxRotation;

    // Start is called before the first frame update
    void Start()
    {
        this.skyboxRotation = 0.0f;
    }

    private void OnValidate() {
        //bool shouldBeUpdating = Application.isEditor && this.updateInEditMode && !Application.isPlaying;

        //if (shouldBeUpdating && TimeTheConceptOf.editorUpdateTimer == null) {
        //    TimeTheConceptOf.editorUpdateTimer = new Timer(100);
        //    TimeTheConceptOf.editorUpdateTimer.Elapsed += (sender, args) => {
        //        this.UpdateSkybox();
        //    };
        //    TimeTheConceptOf.editorUpdateTimer.Start();
        //    Debug.Log("Starting editor Update Timer", this);
        //} else if (!shouldBeUpdating && TimeTheConceptOf.editorUpdateTimer != null) {
        //    TimeTheConceptOf.editorUpdateTimer.Stop();
        //    TimeTheConceptOf.editorUpdateTimer = null;
        //    Debug.Log("Stopping editor Update Timer", this);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        this.UpdateSkybox();
        //if (!Application.isEditor || this.updateInEditMode || Application.isPlaying) {
        //}
    }

    private void UpdateSkybox() {
        this.skyboxRotation += Time.deltaTime * this.skyboxRotationRate;
        RenderSettings.skybox.SetFloat("_Rotation", this.skyboxRotation);
    }
}
