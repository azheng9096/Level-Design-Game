using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    [SerializeField] Canvas dialogueCanvas;
    [SerializeField] TextMeshProUGUI textDisplay;

    [SerializeField] float displayDuration = 1f;
    [SerializeField] float fadeDuration = 0.5f;

    Coroutine currDisplayCoroutine;

    GameObject player;
    [SerializeField] Vector3 offset = new Vector3(0.1f, 1.2f, 0);


    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dialogueCanvas.transform.position = player.transform.position + offset;
    }

    IEnumerator DisplayTextIE(string dialogue) {
        // display text
        textDisplay.text = dialogue;
        textDisplay.color = new Color(textDisplay.color.r, textDisplay.color.g, textDisplay.color.b, 1f);
        yield return new WaitForSeconds(displayDuration);

        // fade text
        float currentTime = 0f;
        while (currentTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, currentTime / fadeDuration);
            textDisplay.color = new Color(textDisplay.color.r, textDisplay.color.g, textDisplay.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }

        // end
        yield break;
    }

    public void DisplayText(string dialogue) {
        if (currDisplayCoroutine != null) {
            StopCoroutine(currDisplayCoroutine);
        }

        currDisplayCoroutine = StartCoroutine(DisplayTextIE(dialogue));
    }
}
