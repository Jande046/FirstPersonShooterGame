using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("KeyBinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("GroundCheck")]
    public float playerHeight;
     public LayerMask whatIsGround;
    bool grounded;
    
    public Transform orientation;
    
    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    Rigidbody rb;

void Start()
{
    rb = GetComponent<Rigidbody>();
    rb.freezeRotation = true;

    readyToJump = true;
    ResetJump();
}

  // Update is called once per frame
private void Update()
{
    
    //ground check
    grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
    
    MyInput();
    SpeedControl();

    //apply drag
    if (grounded)
    {
        rb.drag = groundDrag;

    }
    else
    {
        rb.drag = 0;
    }
}

private void FixedUpdate()
{
    MovePlayer();
}

private void MyInput()
{
    horizontalInput = Input.GetAxisRaw("Horizontal");
    verticalInput = Input.GetAxisRaw("Vertical");

    //when to Jump
    if(Input.GetKey(jumpKey) && readyToJump && grounded)
    {
        readyToJump = false;
        Jump();
        Invoke(nameof(ResetJump), jumpCooldown);
    }

}

private void MovePlayer()  
{
    //calculate move directtion
    moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
    //on Ground
    if(grounded)
    {
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }
    else if(!grounded)
    {
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    

}

    

private void SpeedControl()
{
    Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

    //limit velocity
    if(flatVel.magnitude > moveSpeed)
    {
        Vector3 limitedVel = flatVel.normalized * moveSpeed;
        rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
    }
}

private void Jump()
{
    //reset y velocity
    rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
    rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
}

private void ResetJump()
{
    readyToJump = true;
}

}
