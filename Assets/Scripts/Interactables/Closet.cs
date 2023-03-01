using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closet : MonoBehaviour
{
    bool canInteract = false;
    bool hidingPlayer = false;

    bool cooldown = false;

    Transform exitPosition;

    PlayerController player;

    
    // optional camera offset change
    [SerializeField] bool overrideCameraOffset = false;
    [SerializeField] Vector3 newOffset = new Vector3(0, 5, -10);
    CameraFollow cam;

    // Start is called before the first frame update
    void Start()
    {
        exitPosition = transform.GetChild(0).transform;
        player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerController>();
    
        if (overrideCameraOffset) {
            cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((canInteract || hidingPlayer) && !cooldown && Input.GetKey(KeyCode.Z)) {
            StartCoroutine(Cooldown(0.5f));

            if (!hidingPlayer) {
                hidingPlayer = true;
                player.Hide(true);
                player.gameObject.transform.position = transform.position;

                if (overrideCameraOffset && cam != null) {
                    cam.SetNewOffset(newOffset);
                }
            } else {
                hidingPlayer = false;
                player.gameObject.transform.position = exitPosition.position;
                player.Hide(false);

                if (overrideCameraOffset && cam != null) {
                    cam.ResetOffset();
                }
            }
        }
    }

    IEnumerator Cooldown(float duration) {
        cooldown = true;
        yield return new WaitForSeconds(duration);
        cooldown = false;
    }

    void OnCollisionEnter2D(Collision2D other) {
        // cannot enter closet from back side
        if (other.collider.CompareTag("Player")) { // && other.contacts[0].normal.y < 0.5f
            canInteract = true;
        }
    }

    void OnCollisionExit2D(Collision2D other) {
        if (other.collider.CompareTag("Player")) {
            canInteract = false;
        }
    }
}
