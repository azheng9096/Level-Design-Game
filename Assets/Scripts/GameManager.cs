using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    List<MoveableObject> moveableObjects = new List<MoveableObject>();

    PlayerController player;

    [SerializeField] GameObject EndGamePanel;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        // jank, but fix later maybe
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            ResetAllMoveableObjectsPosition();
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            ResetLevel();   
        }
    }

    public void AddMoveableObject(MoveableObject moveableObject) {
        moveableObjects.Add(moveableObject);
    }

    public void ResetAllMoveableObjectsPosition() {
        foreach (MoveableObject m in moveableObjects) {
            m.ResetPositionToSpawnpoint();
        }
    }

    public void EndGame() {
        Time.timeScale = 0;
        EndGamePanel.SetActive(true);
    }

    public void RetryFromSpawnpoint() {
        player.ResetPositionToSpawnpoint();
    }

    public void ResetLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Replay() {
        SceneManager.LoadScene(0);
    }
}
