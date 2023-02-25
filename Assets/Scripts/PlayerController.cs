using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5;
    Rigidbody2D rb;

    Vector2 movement;

    bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        movement = new Vector2(x, y).normalized;
    }

    void FixedUpdate() {
        if (canMove)
            rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
            // rb.velocity = new Vector2(movement.x * speed, movement.y * speed);
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
}
