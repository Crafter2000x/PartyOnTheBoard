using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GamePlayerInformation MainPlayerInfo;
    public static PlayerMovement Instance;

    public Rigidbody rb;

    public float forwardForce = 1000f;
    public float sidewaysForce = 800f;

    public KeyCode forward;
    public KeyCode backward;
    public KeyCode left;
    public KeyCode right;

    public int score;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (MiniGameManager.Instance.team1Win)
        {
            if (rb.tag == "Player1")
            {
                rb.MovePosition(new Vector3(14, 6.1f, -822));
            }
            if (rb.tag == "Player2")
            {
                rb.MovePosition(new Vector3(8, 6.1f, -822));
            }
            if (rb.tag == "Player3")
            {
                rb.MovePosition(new Vector3(-1, 2.6f, -822));
            }
            if (rb.tag == "Player4")
            {
                rb.MovePosition(new Vector3(-7, 2.6f, -822));
            }
        }
        else if (MiniGameManager.Instance.team2Win)
        {
            if (rb.tag == "Player3")
            {
                rb.MovePosition(new Vector3(14, 6.1f, -822));
            }
            if (rb.tag == "Player4")
            {
                rb.MovePosition(new Vector3(8, 6.1f, -822));
            }
            if (rb.tag == "Player1")
            {
                rb.MovePosition(new Vector3(-1, 2.6f, -822));
            }
            if (rb.tag == "Player2")
            {
                rb.MovePosition(new Vector3(-7, 2.6f, -822));
            }
        }
    }

    void FixedUpdate()
    {
        if (MiniGameManager.Instance.gameStarted)
        {
            Movement();
        }
    }

    public void Movement()
    {
        if (rb.gameObject.tag == "Player1" || rb.gameObject.tag == "Player2" || rb.gameObject.tag == "Player3" || rb.gameObject.tag == "Player4")
        {
            if (MainPlayerInfo != null)
            {
                if (MainPlayerInfo.PlayerInput.Movement.y > 0)
                {
                    rb.AddForce(0, 0, forwardForce * Time.deltaTime);
                }

                if (MainPlayerInfo.PlayerInput.Movement.y < 0)
                {
                    rb.AddForce(0, 0, -forwardForce * Time.deltaTime);
                }

                if (MainPlayerInfo.PlayerInput.Movement.x > 0)
                {
                    rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0);
                }

                if (MainPlayerInfo.PlayerInput.Movement.x < 0)
                {
                    rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0);
                }
            }
        }
    }

    bool coroutineCheck = false;

    public IEnumerator SpeedEffect(bool black)
    {
        coroutineCheck = true;

        if (black)
        {
            forwardForce = 1500;
            sidewaysForce = 1300;
            yield return new WaitForSeconds(7f);
            forwardForce = 1000;
            sidewaysForce = 800;
        }
        else if (!black)
        {
            forwardForce = 500;
            sidewaysForce = 300;
            yield return new WaitForSeconds(7f);
            forwardForce = 1000;
            sidewaysForce = 800;
        }
        
        coroutineCheck = false;
    }

    public void Speedboost(bool black)
    {
        if (!coroutineCheck)
        {
            StartCoroutine(SpeedEffect(black));
        }
    }
}
