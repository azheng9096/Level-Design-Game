using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Item unlockItem;

    // audio
    [SerializeField] AudioClip lockedAudio;
    [SerializeField] AudioClip openAudio;

    AudioSource audioSource;

    // interaction
    bool canInteract = false;

    // particle
    [SerializeField] ParticleSystem particle;

    // dialogue
    [SerializeField] string lockedDialogue;

    // other instance
    [SerializeField] Door otherInstance;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.Z)){
            if (unlockItem != null && InventoryManager.instance.inventory.Contains(unlockItem)) {
                // remove item from inventory
                InventoryManager.instance.RemoveItem(unlockItem);

                // play open audio
                if (audioSource != null && openAudio != null) {
                    audioSource.PlayOneShot(openAudio);
                }

                // play particle effect
                if (particle != null) {
                    particle.Play();
                    particle.gameObject.transform.SetParent(null);
                }

                // destroy the other instance if it exists
                if (otherInstance != null)
                    Destroy(otherInstance.gameObject);

                Destroy(gameObject);
            } else {
                DialogueManager.instance.DisplayText(lockedDialogue);

                // play lock audio
                if (audioSource != null && lockedAudio != null) {
                    audioSource.PlayOneShot(lockedAudio);
                }
            }  
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.CompareTag("Player"))
            canInteract = true;
    }

    void OnCollisionExit2D(Collision2D other) {
        if (other.collider.CompareTag("Player"))
            canInteract = false;
    }
}
