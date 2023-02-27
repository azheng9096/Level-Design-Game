using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePickUpTriggerLocked : InteractablePickUp
{
    [SerializeField] int numTriggers;
    int currTriggered = 0;

    // audio
    [SerializeField] AudioClip unlockAudio;
    [SerializeField] AudioClip lockedAudio;

    // particle
    [SerializeField] bool breakOnPickup = false;
    [SerializeField] ParticleSystem particle;


    // dialogue
    [SerializeField] string lockedDialogue;

    delegate void OnCurrKeysChanged();
    OnCurrKeysChanged OnUnlockCallback; // triggers when currTriggered == numTriggers

    protected override void Start()
    {
        base.Start();

        OnUnlockCallback += PlayUnlockedSound;
    }

    // Update is called once per frame
    void Update()
    {
        if (canPickUp && Input.GetKeyDown(KeyCode.Z)) {
            if (item == null) return;

            if (numTriggers <= currTriggered) {
                if (!dontDestroyOnPickup && breakOnPickup) {
                    // play particle effect
                    if (particle != null) {
                        particle.Play();
                        particle.gameObject.transform.SetParent(null);
                    }
                }

                PickUp();
            }
            else {
                DialogueManager.instance.DisplayText(lockedDialogue);
                
                if (audioSource != null && lockedAudio != null)
                    audioSource.PlayOneShot(lockedAudio);
            }
        }
    }

    void PlayUnlockedSound() {
        Debug.Log("Item Unlocked!");
        if (audioSource != null && unlockAudio != null) {
            audioSource.PlayOneShot(unlockAudio);
        }
    }

    public void IncrementTriggered() {
        currTriggered++;

        if (currTriggered == numTriggers)
            OnUnlockCallback?.Invoke();
    }

    public void DecrementTriggered() {
        currTriggered--;

        if (currTriggered == numTriggers)
            OnUnlockCallback?.Invoke();
    }
}
