using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] string nextLevelName;
    [SerializeField] bool lastLevel;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            if (lastLevel) {
                GameManager.instance.EndGame();
                return;
            }

            if (nextLevelName != "")
                SceneManager.LoadScene(nextLevelName);
        }
    }
}
