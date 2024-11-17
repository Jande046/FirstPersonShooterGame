using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;
    private PlayerMotor motor;
    private PlayerLook look;
    private Weapon weapon;
    //private PlayerShoot shoot;
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        weapon = GetComponent<Weapon>();
        onFoot.Jump.performed += ctx => motor.Jump();
        onFoot.Sprint.performed += ctx => motor.Sprint();
        onFoot.Shoot.performed += ctx => weapon.Shoot();
        
    }


    // Update is called once per frame
    void Update()
    {
        //tell player motor to move using value from our movement action
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
         if (onFoot.Shoot.ReadValue<float>() > 0) // Fire button is pressed
    {
        weapon.Shoot();
    }

    }

    private void LateUpdate()
    {
         look.ProcessLook(onFoot.Look.ReadValue<Vector2>());

    }
    private void OnEnable()
    {
        onFoot.Enable();

    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}
