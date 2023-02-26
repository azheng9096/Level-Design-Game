using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePickUp : MonoBehaviour
{
    [SerializeField] Item item;

    bool canPickUp = false;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("Player")?.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canPickUp && Input.GetKeyDown(KeyCode.Z)) {
            PickUp();
        }
    }

    void PickUp() {
        canPickUp = false;

        // add item to inventory
        InventoryManager.instance.AddItem(item);

        // play pick up audio
        if (audioSource != null && item.pickUpAudio != null) {
            audioSource.PlayOneShot(item.pickUpAudio);
        }

        // destroy object
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            canPickUp = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            canPickUp = false;
        }
    }
}
