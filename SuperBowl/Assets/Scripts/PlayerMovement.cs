using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerMovement : MonoBehaviour
{
    public GameObject cameraObject;
    public float acceleration;
    public float walkAccelerationRatio;
    public float maxWalkSpeed;
    public float deaccelerate = 2;
    [HideInInspector]
    public Vector2 horizontalMovement;
    [HideInInspector]
    public float walkDeaccelerateX;
    [HideInInspector]
    public float walkDeaccelerateZ;
    [HideInInspector]
    public bool isGrounded = true;
    Rigidbody playerRigidBody;
    public float jumpVelocity = 20;
    float maxSlope = 45;

    void Awake()
    {
        //gets rigidbody component from player
        playerRigidBody = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
   /* void Start()
    {
        
    } */

    // Update is called once per frame
    void Update()
    {
        jump();
        Move();   
    }
    void jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerRigidBody.AddForce(0, jumpVelocity, 0);
        }
    }

    void Move()
    {
        //controls the limit of player by measuring teh Vector 3 magnitude and measuring and normalizng that vector
        horizontalMovement = new Vector2(playerRigidBody.velocity.x, playerRigidBody.velocity.z);
        
        if(horizontalMovement.magnitude > maxWalkSpeed)
        {
            horizontalMovement = horizontalMovement.normalized;
            horizontalMovement *= maxWalkSpeed;
        }
        playerRigidBody.velocity = new Vector3(horizontalMovement.x, playerRigidBody.velocity.y, horizontalMovement.y);

        //rotating player capsule according to MouseLook
        transform.rotation = Quaternion.Euler(0, cameraObject.GetComponent<MouseLook>().currentY, 0);
        if(isGrounded)
        {
            playerRigidBody.AddRelativeForce(Input.GetAxis("Horizontal") * acceleration, 0, Input.GetAxis("Vertical") * acceleration);
        
        }
        else
        {
            playerRigidBody.AddRelativeForce(Input.GetAxis("Horizontal") * acceleration * walkAccelerationRatio, 0, Input.GetAxis("Vertical") * walkAccelerationRatio * acceleration);

        }
        if(isGrounded)
        {
            float xMove = Mathf.SmoothDamp(playerRigidBody.velocity.x, 0, ref walkDeaccelerateX, deaccelerate);
            float zMove = Mathf.SmoothDamp(playerRigidBody.velocity.z, 0, ref walkDeaccelerateZ, deaccelerate);

        }  
    }
    void OnCollisionEnter(Collision coll)
    {
        foreach(ContactPoint contact in coll.contacts)
        if(Vector3.Angle(contact.normal, Vector3.up) < maxSlope)
            isGrounded = true;
    }
    void OnCollisionExit(Collision coll)
    {
        if(coll.gameObject.name.Equals("Plane")){
            isGrounded = false;
        }
    }
}
