using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ToggleVRButton : MonoBehaviour {
    public Button button;
    public GameObject player;
    private WalkController walkController;

    public Color trueColor;
    public Color falseColor;

    protected void Start() {
        if (this.button == null) {
            Debug.LogError("No button assigned", this);
            return;
        }
        if (this.player == null) {
            Debug.LogError("No player assigned", this);
            return;
        }

        this.walkController = this.player.GetComponent<WalkController>();
        if (this.walkController == null) {
            Debug.LogError("No WalkController assigned to player " + this.player, this);
            return;
        }
    }

    private void Update() {
        this.OnValueUpdated();
    }

    void OnValueUpdated() {
        // TODO: only on value changed as per before
        if (this.button == null || this.walkController == null) {
            return;
        }

        Color useColor;
        if (this.walkController.IsUsingVR == true) {
            useColor = this.trueColor;
        } else {
            useColor = this.falseColor;
        }

        ColorBlock colorBlock = this.button.colors;
        colorBlock.highlightedColor = useColor;
        this.button.colors = colorBlock;
    }
}
