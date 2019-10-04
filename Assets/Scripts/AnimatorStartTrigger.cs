using UnityEngine;
using System.Collections;

public class AnimatorStartTrigger : MonoBehaviour {
    public string triggerName;

    Animator animator;
    // Start is called before the first frame update

    void Start() {
        this.animator = GetComponent<Animator>();
        if (this.animator == null) {
            Debug.LogError("Unable to retrieve animator for " + this);
            return;
        }

        this.animator.SetTrigger(triggerName);
    }

    // Update is called once per frame
    void Update() {

    }
}
