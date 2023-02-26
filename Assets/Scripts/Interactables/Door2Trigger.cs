using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door2Trigger : MonoBehaviour
{
    [SerializeField] string objectTag; // tag name of object that can trigger this trigger
    [SerializeField] Door2 door; // door that this trigger is connected to

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag(objectTag) && door != null) {
            door.IncrementTriggered();
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag(objectTag) && door != null) {
            door.DecrementTriggered();
        }
    }
}
