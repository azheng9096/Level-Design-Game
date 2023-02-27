using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePickUp : MonoBehaviour
{
    [SerializeField] protected Item item;

    protected bool canPickUp = false;

    protected AudioSource audioSource;

    [SerializeField] protected bool dontDestroyOnPickup = false;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("Player")?.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canPickUp && Input.GetKeyDown(KeyCode.Z)) {
            if (item != null)
                PickUp();
        }
    }

    protected virtual void PickUp() {
        canPickUp = false;

        // add item to inventory
        InventoryManager.instance.AddItem(item);

        // play pick up audio
        if (audioSource != null && item.pickUpAudio != null) {
            audioSource.PlayOneShot(item.pickUpAudio);
        }

        if (dontDestroyOnPickup) {
            item = null;
            return;
        }

        // destroy object
        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            canPickUp = true;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            canPickUp = false;
        }
    }


    protected void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.CompareTag("Player")) {
            canPickUp = true;
        }
    }

    protected void OnCollisionExit2D(Collision2D other) {
        if (other.collider.CompareTag("Player")) {
            canPickUp = false;
        }
    }
}
