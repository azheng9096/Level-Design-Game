using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Patrol : MonoBehaviour
{
    [SerializeField] Transform[] waypoints;
    int currWaypointIndex = 0;
    float patrolSpeed = 2f;

    float waitTime = 1f;

    Coroutine prevCoroutine;

    // fov
    [SerializeField] Transform fovPrefab;
    FieldOfView fieldOfView;
    [SerializeField] float fov = 90f;
    [SerializeField] float viewDistance = 10f;

    Rigidbody2D rb;

    GameObject player;

    void Start()
    {
        prevCoroutine = StartCoroutine(MovingToNextWaypoint());

        fieldOfView = Instantiate(fovPrefab, null).GetComponent<FieldOfView>();
        fieldOfView.SetFov(fov);
        fieldOfView.SetViewDistance(viewDistance);

        rb = GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {
        fieldOfView.SetOrigin(transform.position);
        fieldOfView.SetAimDirection(transform.right);

        FIndTargetPlayer();
    }

    IEnumerator MovingToNextWaypoint()
    {
        Transform wp = waypoints[currWaypointIndex];

        while (Vector3.Distance(transform.position, wp.position) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, wp.position, patrolSpeed * Time.deltaTime);
            transform.right = wp.position - transform.position;
            yield return null;
        }

        transform.position = wp.position;
        yield return new WaitForSeconds(waitTime);
        currWaypointIndex = (currWaypointIndex + 1) % waypoints.Length;

        StopCoroutine(prevCoroutine);
        prevCoroutine = StartCoroutine(MovingToNextWaypoint());
    }

    void FIndTargetPlayer() {
        if(Vector3.Distance(transform.position, player.transform.position) < viewDistance) {
            // Player inside view distance
            Vector2 dirToPlayer = (player.transform.position - transform.position).normalized;
            Vector3 aimDirection = transform.right;
            if (Vector3.Angle(aimDirection, dirToPlayer) < fov / 2f) {
                // Player inside FOV
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
