using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pet : MonoBehaviour, IUsable {
    public AudioClip[] petAudios;
    private AudioSource audioSource;
    private int lastPetAudioClipIndex = 0;
    private bool queueNextPetAudioClipWhenFinishedPlaying;

    private void Start() {
        this.audioSource = this.gameObject.AddComponent<AudioSource>();
        this.audioSource.playOnAwake = false;

        this.QueueNextAudioClip();
    }

    private void Update() {
        if (this.queueNextPetAudioClipWhenFinishedPlaying && !this.audioSource.isPlaying) {
            this.queueNextPetAudioClipWhenFinishedPlaying = false;
            this.QueueNextAudioClip();
        }
    }

    public void Use() {
        if (this.audioSource.isPlaying) {
            return;
        }
        // TODO: animate camera
        // TODO: animate animal
        this.audioSource.Play();
        this.queueNextPetAudioClipWhenFinishedPlaying = true;
    }

    private void OnDestroy() {
        Destroy(this.audioSource);
    }

    private void QueueNextAudioClip() {
        if (this.petAudios == null || this.petAudios.Length == 0) {
            Debug.LogWarning("Pet Audio is unassigned", this);
            return;
        }

        if (this.petAudios.Length == 1) {
            this.audioSource.clip = this.petAudios[0];
        } else {
            int index = Random.Range(0, this.petAudios.Length);
            if (index == this.lastPetAudioClipIndex) {
                index++;
            }
            if (index >= this.petAudios.Length) {
                index = 0;
            }
            this.audioSource.clip = this.petAudios[index];
            this.lastPetAudioClipIndex = index;
        }
    }
}
