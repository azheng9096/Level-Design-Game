using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform target;
    [SerializeField] Vector3 offset = new Vector3(0, 2, -10);
    [SerializeField] float smoothTime = 0.25f;
    Vector3 currentVelocity;

    void Start() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        if (target != null) {
            transform.position = Vector3.SmoothDamp (
                transform.position, 
                target.position + offset, 
                ref currentVelocity, 
                smoothTime
            );
        }
    }
}
