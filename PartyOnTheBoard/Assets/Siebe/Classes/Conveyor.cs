using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float startSpeed;
    private bool isArrowKeyPressed;
    public float scrollX = 0.5f;
    public float scrollY = 0.5f;
    private Rigidbody rb;
    private Conveyor conveyor;
    [SerializeField] Player Player;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        conveyor = GetComponent<Conveyor>();
    }

    void Update()
    {
        if (MinigameManager.instance.countDown < 0)
        {
            StartSpeed();
            float offsetX = Time.time * scrollX;
            float offsetY = Time.time * scrollY;
            GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offsetX, offsetY);
            if (!isArrowKeyPressed)
            {
                scrollY = -0.05f;
            }
        }
    }

    void FixedUpdate()
    {
        if (MinigameManager.instance.countDown < 0)
        {
            conveyor.MoveBackward();
            //conveyor.MoveForward();
            conveyor.ChangeTexture();
            conveyor.ChangeSpeed(15);
            //Debug.Log(scrollY);
        }
    }

    public Conveyor()
    {
        
    }

    void MoveForward()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            isArrowKeyPressed = true;
            Vector3 pos = rb.position;
            rb.position += Vector3.back * speed * Time.fixedDeltaTime;
            rb.MovePosition(pos);
            //scrollY = -0.166666666665f;
        }
        else
        {
            isArrowKeyPressed = false;
        }
    }

    void MoveBackward()
    {
        if (Player.GamePlayerInformation.PlayerInput.Movement.x < 0)
        {
            isArrowKeyPressed = true;
            Vector3 pos = rb.position;
            rb.position += Vector3.forward * speed * Time.fixedDeltaTime;
            rb.MovePosition(pos);
            //scrollY = -0.166666666665f;
        }
        else
        {
            isArrowKeyPressed = false;
        }
    }

    void ChangeTexture()
    {

        if (Input.GetKey(KeyCode.DownArrow))
        {
            scrollY = -0.166666666665f;
        }
        //else if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    scrollY = 0.166666666665f;
        //}
    }

    void ChangeSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    void StartSpeed()
    {
        Vector3 pos = rb.position;
        rb.position += Vector3.forward * startSpeed * Time.fixedDeltaTime;
        rb.MovePosition(pos);
    }

    public override string ToString()
    {
        return ("The speed of this conveyor is: " + speed);
    }

}
