using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour, IUsable {

    public AudioClip eatAudio;
    private AudioSource audioSource;
    private bool waitingForAudioToDestroy = false;

    private void Start() {
        this.audioSource = this.gameObject.AddComponent<AudioSource>();
        if (this.eatAudio != null) {
            this.audioSource.clip = this.eatAudio;
        } else { 
            Debug.LogWarning("Eat Audio is unassigned", this);
        }
    }

    public void Use() {
        // TODO: animate picking up
        // TODO: animate eating chunks and moving to and from mouth
        // TODO: munch sound
        this.audioSource.Play();
        this.waitingForAudioToDestroy = true;
    }

    private void Update() {
        if (this.waitingForAudioToDestroy && !this.audioSource.isPlaying) {
            Destroy(this.gameObject);

        }
    }
}
