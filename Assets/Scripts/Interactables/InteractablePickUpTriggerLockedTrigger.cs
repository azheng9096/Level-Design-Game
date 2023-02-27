using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePickUpTriggerLockedTrigger : MonoBehaviour
{
    [SerializeField] string objectTag; // tag name of object that can trigger this trigger
    [SerializeField] InteractablePickUpTriggerLocked interactable; // interactable that this trigger is connected to

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag(objectTag) && interactable != null) {
            interactable.IncrementTriggered();
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag(objectTag) && interactable != null) {
            interactable.DecrementTriggered();
        }
    }
}
