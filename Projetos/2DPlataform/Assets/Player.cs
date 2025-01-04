using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public Rigidbody2D rigidbody2;
    public Vector2 friction = new Vector2(.1f, 0);
    public float speed;
    public float speedRun;
    public float forceJump = 2;

    private float _currentSpeed;
    private bool _isRunning;

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleJump();
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody2.velocity = Vector2.up * forceJump;
        }
    }

    private void HandleMovement()
    {
        _isRunning = Input.GetKey(KeyCode.LeftShift);

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigidbody2.velocity = new Vector2(_isRunning ? -speedRun : -speed, rigidbody2.velocity.y);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rigidbody2.velocity = new Vector2(_isRunning ? speedRun : speed, rigidbody2.velocity.y);
        }

        if (rigidbody2.velocity.x > 0)
        {
            rigidbody2.velocity += friction;
        }
        else if (rigidbody2.velocity.x < 0)
        {
            rigidbody2.velocity -= friction;
        }
    }

}
