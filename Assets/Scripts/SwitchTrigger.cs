using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTrigger : MonoBehaviour
{
    public bool disabled;
    
    [SerializeField] Transform toSwitchTrigger;
    Transform toSwitchTriggerSpawnpoint;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        toSwitchTriggerSpawnpoint = toSwitchTrigger.transform.GetChild(0).transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && !disabled) {
            player.transform.position = toSwitchTriggerSpawnpoint.position;

            // temporarily disable player movement to prevent player from walking into switch trigger again
            // also grants time for animation if necessary
            player.GetComponent<PlayerController>().TempDisableMovement(1.5f);
        }
    }
}
