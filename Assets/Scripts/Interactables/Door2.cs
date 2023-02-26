using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door2 : MonoBehaviour
{
    [SerializeField] int numTriggers;
    int currTriggered = 0;

    // audio
    [SerializeField] AudioClip lockedAudio;
    [SerializeField] AudioClip openAudio;
    [SerializeField] AudioClip unlockAudio;

    AudioSource audioSource;

    // interaction
    bool canInteract = false;

    // particle
    [SerializeField] ParticleSystem particle;
    
    
    // alt
    [SerializeField] bool autoBreakOnUnlock = false;
    // [SerializeField] GameObject particlePrefab;
    [SerializeField] ParticleSystem particleRespawnable;
    SpriteRenderer sprite;
    BoxCollider2D boxCollider2D;


    delegate void OnCurrKeysChanged();
    OnCurrKeysChanged OnUnlockCallback; // triggers when currTriggered == numTriggers
    OnCurrKeysChanged OnCurrKeysChangedCallback; 


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("Player")?.GetComponent<AudioSource>();

        OnUnlockCallback += PlayDoorUnlockedSound;

        sprite = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        if (autoBreakOnUnlock) {
            OnCurrKeysChangedCallback += DoorSimulate;
        }
    }

    void OnDestroy() {
        OnUnlockCallback -= PlayDoorUnlockedSound;

        if (autoBreakOnUnlock) {
            OnCurrKeysChangedCallback -= DoorSimulate;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (autoBreakOnUnlock) {
            return;
        }
        
        if (canInteract && Input.GetKeyDown(KeyCode.Z)){
            if (numTriggers <= currTriggered) {
                // play open audio
                if (audioSource != null && openAudio != null) {
                    audioSource.PlayOneShot(openAudio);
                }

                // play particle effect
                if (particle != null) {
                    particle.Play();
                    particle.gameObject.transform.SetParent(null);
                }

                Destroy(gameObject);
            } else {
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

    public void IncrementTriggered() {
        currTriggered++;

        OnCurrKeysChangedCallback?.Invoke();
        if (currTriggered == numTriggers)
            OnUnlockCallback?.Invoke();
    }

    public void DecrementTriggered() {
        currTriggered--;

        OnCurrKeysChangedCallback?.Invoke();
        if (currTriggered == numTriggers)
            OnUnlockCallback?.Invoke();
    }

    void PlayDoorUnlockedSound() {
        Debug.Log("Door Unlocked!");
        if (audioSource != null && unlockAudio != null) {
            audioSource.PlayOneShot(unlockAudio);
        }
    }

    void DoorSimulate() {
        if (numTriggers <= currTriggered) {
            // hide door
            sprite.enabled = false;
            boxCollider2D.enabled = false;

            // play break effect
            /*
            if (particlePrefab != null) {
                GameObject particleEffect = Instantiate(particlePrefab, transform.position, Quaternion.identity);
                particleEffect.GetComponent<ParticleSystem>()?.Play();
            }
            */
            if (particleRespawnable != null) {
                particleRespawnable.Clear();
                particleRespawnable.Play();
            }
        } else {
            // show door
            sprite.enabled = true;
            boxCollider2D.enabled = true;
        }
    }
}
