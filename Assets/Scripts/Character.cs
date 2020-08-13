using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Transform visual;
    public GameObject bloodStream;
    public float moveForce;
    public float jumpForce;

    Rigidbody2D rigidBody2D;
    TriggerDetector triggerDetector;
    Animator animator;
    float visualDirection;

    // Start is called before the first frame update
    void Start()
    {
        visualDirection = 1.0f;
        rigidBody2D = GetComponent<Rigidbody2D>();
        triggerDetector = GetComponentInChildren<TriggerDetector>();
        animator = GetComponentInChildren<Animator>();
    }

    public void MoveLeft()
    {
        rigidBody2D.AddForce(new Vector2(-moveForce, 0.0f), ForceMode2D.Impulse);
    }

    public void MoveRight()
    {
        rigidBody2D.AddForce(new Vector2(moveForce, 0.0f), ForceMode2D.Impulse);
    }

    public void Jump()
    {
        if (triggerDetector.inTrigger)
            rigidBody2D.AddForce(new Vector2(0.0f, jumpForce), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<MovingPlatform>() != null)
            transform.SetParent(collision.transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<MovingPlatform>() != null)
            transform.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
        float velocity = rigidBody2D.velocity.x;

        if (velocity < -0.1f) {
            visualDirection = -1.0f;
        } else if (velocity > 0.1f) {
            visualDirection = 1.0f;
        }

        Vector3 scale = visual.localScale;
        scale.x = visualDirection;
        visual.localScale = scale;

        animator.SetFloat("speed", Mathf.Abs(velocity));
    }
}
