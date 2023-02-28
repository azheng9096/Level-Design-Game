using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePickUpTriggerLockedTrigger : MonoBehaviour
{
    [SerializeField] string objectTag; // tag name of object that can trigger this trigger
    [SerializeField] InteractablePickUpTriggerLocked interactable; // interactable that this trigger is connected to

    [SerializeField] Sprite releasedSprite;
    [SerializeField] Sprite onPressedSprite;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag(objectTag) && interactable != null) {
            interactable.IncrementTriggered();

            if (onPressedSprite != null) {
                GetComponent<SpriteRenderer>().sprite = onPressedSprite;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag(objectTag) && interactable != null) {
            interactable.DecrementTriggered();

            if (releasedSprite != null) {
                GetComponent<SpriteRenderer>().sprite = releasedSprite;
            }
        }
    }
}
