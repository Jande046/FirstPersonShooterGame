using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [Header("MovementSettings")]
    private CharacterController controller;
    private Vector3 playerVelocity;
    public float speed = 5f;
    bool sprinting = false;
    [Header("GravitySettings")]
    private bool isGrounded;
    public float gravity = -9.8f;
    [Header("JumpSettings")]
    public float jumpHeight = 3f;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
        
    }
    
    //recieve the inputs for our InputManger.cs adn apply them to our character controller
    public void ProcessMove(Vector2 Input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = Input.x;
        moveDirection.z = Input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        controller.Move(playerVelocity * Time.deltaTime);
        Debug.Log(playerVelocity.y);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3 * gravity);
        }
    }

    public void Sprint()
    {
        sprinting = !sprinting;
        if(sprinting)
            speed = 14;
        else
            speed = 8;
    }
}
