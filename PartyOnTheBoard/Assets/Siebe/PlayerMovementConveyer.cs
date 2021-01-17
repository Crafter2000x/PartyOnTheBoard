using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementConveyer : MonoBehaviour
{
    public CharacterController controller;
    public Conveyor conveyor;
    public PlayerMovementConveyer player;
    Rigidbody rb;
    float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;
    public float speed = 6;
    Vector3 velocity;
    public float gravity = -9.81f;
    bool IsSpacePressed;
    bool isGrounded;
    public float conveyorEffect;
    public float jumpHeight = 5;
    public float groundDistance = 0.4f;

    void Start()
    {
        player = GetComponent<PlayerMovementConveyer>();
        conveyor = GetComponent<Conveyor>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IsSpacePressed = true;
        }
    }

    /*void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Raakt Band");
            //rb.velocity = conveyor.speed * transform.forward;
        }
    }*/

    void FixedUpdate()
    {
        //jump

        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundDistance + 0.1f);

        if (IsSpacePressed == true)

            if (isGrounded && velocity.y < 0)
            {

                velocity.y = -2f;

            }

        if (!isGrounded)
        {
            IsSpacePressed = false;
        }


        if (IsSpacePressed && isGrounded)
        {
            IsSpacePressed = false;
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);

        }
        //Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        //Walk
        
        float horizontal = Input.GetAxisRaw("Horizontal2");

        float vertical = Input.GetAxisRaw("Vertical2");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;



        if (direction.magnitude >= 0.1f)
        {

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);



            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move(moveDir.normalized * speed * Time.deltaTime);

        }
    }
}

