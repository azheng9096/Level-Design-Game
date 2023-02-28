using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    Vector3 spawnpoint;

    // Start is called before the first frame update
    void Start()
    {
        spawnpoint = transform.position;

        GameManager.instance.AddMoveableObject(this);
    }

    public void ResetPositionToSpawnpoint() {
        transform.position = spawnpoint;
    }
}
