using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class JoystickMovement : MonoBehaviour
{
    private MyJoystick myJoy;

    [SerializeField]
    FloatingJoystick dJoy;

    private CharacterController controller;
    public float characterSpeed;


    private float turnInput;
    private float moveInput;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;


    Animator animator;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        myJoy = FindObjectOfType<MyJoystick>();
    }


    private void Update()
    {

        turnInput = dJoy.Horizontal;
        moveInput = dJoy.Vertical;


        if (myJoy.pressed == true)
        {
            Move();
        }
        else
        {


            controller.Move((Vector3.zero));
        }
    }

    void Move()
    {
        Vector3 direction = new Vector3(turnInput, 0f, moveInput).normalized;

        if (direction.magnitude >= 0.1f)
        {

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * (characterSpeed * Time.deltaTime));

        }

    }





}
