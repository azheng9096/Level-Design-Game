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
    PlayerController playerController;

    // for animation
    [SerializeField] GameObject spriteHolder;
    SpriteRenderer sprite;
    Animator animator;
    [SerializeField] bool spriteIsFacingRight = true;

    void Start()
    {
        prevCoroutine = StartCoroutine(MovingToNextWaypoint());

        fieldOfView = Instantiate(fovPrefab, null).GetComponent<FieldOfView>();
        fieldOfView.SetFov(fov);
        fieldOfView.SetViewDistance(viewDistance);

        rb = GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            playerController = player.GetComponent<PlayerController>();

        if (spriteHolder != null) {
            sprite = spriteHolder.GetComponent<SpriteRenderer>();
            animator = spriteHolder.GetComponent<Animator>();
        }
    }

    void Update() {
        fieldOfView.SetOrigin(transform.position);
        fieldOfView.SetAimDirection(transform.right);

        FindTargetPlayer();
    }

    IEnumerator MovingToNextWaypoint()
    {
        Transform wp = waypoints[currWaypointIndex];

        while (Vector3.Distance(transform.position, wp.position) > 0.01f)
        {
            float x_diff = wp.position.x - transform.position.x; // positive - right, negative - left

            transform.position = Vector3.MoveTowards(transform.position, wp.position, patrolSpeed * Time.deltaTime);
            transform.right = wp.position - transform.position;

            if (spriteHolder != null) {
                spriteHolder.transform.position = transform.position;

                if (animator != null)
                    animator.SetBool("isMoving", true);

                if (sprite != null) {
                    if (x_diff < 0) 
                        sprite.flipX = spriteIsFacingRight;
                    else if (x_diff > 0) 
                        sprite.flipX = !spriteIsFacingRight;
                }
            }

            yield return null;
        }

        transform.position = wp.position;
        if (spriteHolder != null) {
            spriteHolder.transform.position = transform.position;

            if (animator != null)
                animator.SetBool("isMoving", false);
        }

        yield return new WaitForSeconds(waitTime);
        currWaypointIndex = (currWaypointIndex + 1) % waypoints.Length;

        StopCoroutine(prevCoroutine);
        prevCoroutine = StartCoroutine(MovingToNextWaypoint());
    }

    void FindTargetPlayer() {
        if(playerController.detectable && Vector3.Distance(transform.position, player.transform.position) < viewDistance) {
            // Player inside view distance
            Vector2 dirToPlayer = (player.transform.position - transform.position).normalized;
            Vector3 aimDirection = transform.right;
            if (Vector3.Angle(aimDirection, dirToPlayer) < fov / 2f) {
                // Player inside FOV
                // GameManager.instance.RetryFromSpawnpoint();
            }
        }
    }
}
