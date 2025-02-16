using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidbody2D;
    public Vector2 friction = new Vector2(.1f,0);

    public float speed;
    public float speedRun;

    public float forceJump = 2f;

    private float _currentSpeed;

    // Update is called once per frame
    void Update()
    {

        HandleMovement();
        HandleJump();
    }
    void HandleMovement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _currentSpeed = speedRun;
        }
        else
        {
            _currentSpeed = speed;

        }


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            myRigidbody2D.velocity = new Vector2(-_currentSpeed, myRigidbody2D.velocity.y);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            myRigidbody2D.velocity = new Vector2(_currentSpeed, myRigidbody2D.velocity.y);
        }

        if(myRigidbody2D.velocity.x < 0)
        {
            myRigidbody2D.velocity += friction;
        }
        else if (myRigidbody2D.velocity.x > 0)
        {
            myRigidbody2D.velocity -= friction;
        }
    }
    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody2D.velocity = Vector2.up * forceJump;
        }
    }
}
    

