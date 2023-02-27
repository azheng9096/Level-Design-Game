using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5;
    Rigidbody2D rb;

    Vector2 movement;

    public bool canMove = true;

    public bool detectable = true; // can be detected by enemies

    // [SerializeField] FieldOfView fov;


    // animation
    Animator animator;
    SpriteRenderer sprite;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        movement = new Vector2(x, y).normalized;

        // FOV object has to be at (0, 0, 0) position or else it will not be correctly aligned
        // fov.SetAimDirection(movement);
        // fov.SetOrigin(transform.position);
        // fov.SetFov(90f);
        // fov.SetViewDistance(10f);
        if (canMove && sprite != null && sprite.enabled) {
            if (x < 0) sprite.flipX = true;
            else if (x > 0) sprite.flipX = false;
            animator.SetFloat("Speed", movement.sqrMagnitude);
        } else if (!canMove) {
            animator.SetFloat("Speed", 0);
        }
    }

    void FixedUpdate() {
        if (canMove) {
            rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
            // rb.velocity = new Vector2(movement.x * speed, movement.y * speed);
        }
    }

    IEnumerator TempDisableMovementIE(float duration) {
        canMove = false;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(duration);
        canMove = true;
    }

    public void TempDisableMovement(float duration) {
        StartCoroutine(TempDisableMovementIE(duration));
    }

    public void ToggleMovement(bool val) {
        if (!val) {
            canMove = false;
            rb.velocity = Vector2.zero;
        } else {
            canMove = true;
        }
    }

    public void Hide(bool val) {
        detectable = !val;
        ToggleMovement(!val);
        GetComponent<BoxCollider2D>().enabled = !val;
        GetComponent<SpriteRenderer>().enabled = !val;
    }
}
