using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    [SerializeField] public Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private float JumpHeight = 2f;
    [SerializeField] public bool isPlayerOne;
    [SerializeField] public bool isPlayerTwo;
    [SerializeField] public bool isPlayerThree;
    [SerializeField] public bool isConveyorPlayer;
    Vector3 pos1 = new Vector3(118.35f, 3, 29);
    Vector3 pos2 = new Vector3(115, 3, 29);
    Vector3 pos3 = new Vector3(121.7f, 3, 29);
    Vector3 pos4 = new Vector3(147, 4.5f, 27.55f);
    //Vector3 posAf = new Vector3(-19, 30, 17);
    private bool IsSpacePressed;
    private int i = 0;
    private bool isGrounded = true;
    public GamePlayerInformation GamePlayerInformation;
    [SerializeField] SpawnBox spawnBox;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (GamePlayerInformation != null)
        {
            GamePlayerInformation.PlayerInput.OnVariableChangeInputA += AbuttonPressed;
        }
    }

    private void AbuttonPressed(bool newVal)
    {
        IsSpacePressed = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (MinigameManager.instance.countDown < 0)
        {
            if (!MinigameManager.instance.gameOver)
            {
                Move();
                Jump();
            }
            else if (MinigameManager.instance.gameOver)
            {
                Jump();
            }
        }
        if (MinigameManager.instance.gameOver == true)
        {
            if (MinigameManager.instance.DeadPlayers.Count == 3)
            {
                MinigameManager.instance.MainCam.enabled = false;
                MinigameManager.instance.RankCam2.enabled = true;
                MinigameManager.instance.winnerText.text = "'Conveyor Player' won!";
                MinigameManager.instance.winnerText.enabled = true;
                if (isConveyorPlayer)
                {
                    rb.MovePosition(pos4);
                }
            }
            else
            {
                MinigameManager.instance.MainCam.enabled = false;
                MinigameManager.instance.RankCam1.enabled = true;
                MinigameManager.instance.winnerText.text = "'player1', 'player2' and 'player3' won!";
                MinigameManager.instance.winnerText.enabled = true;
                if (isPlayerOne)
                {
                    rb.MovePosition(pos1);
                }
                else if (isPlayerTwo)
                { 
                    rb.MovePosition(pos2);
                }
                else if (isPlayerThree)
                {
                    rb.MovePosition(pos3);
                }
            }
        }
    }

    public void Move()
    {
        if (GamePlayerInformation != null)
        {
            if (isPlayerOne)
            {
                Vector3 direction = new Vector3(GamePlayerInformation.PlayerInput.Movement.x, 0, GamePlayerInformation.PlayerInput.Movement.y);
                rb.AddForce(direction * speed, ForceMode.Impulse);
            }
            else if (isPlayerTwo)
            {
                Vector3 direction = new Vector3(GamePlayerInformation.PlayerInput.Movement.x, 0, GamePlayerInformation.PlayerInput.Movement.y);
                rb.AddForce(direction * speed, ForceMode.Impulse);
            }
            else if (isPlayerThree)
            {
                Vector3 direction = new Vector3(GamePlayerInformation.PlayerInput.Movement.x, 0, GamePlayerInformation.PlayerInput.Movement.y);
                rb.AddForce(direction * speed, ForceMode.Impulse);
            }
        }
    }

    public void Jump()
    {
        //isGrounded = Physics.Raycast(transform.position, Vector3.down, groundDistance + 0.1f);
        if (isConveyorPlayer == true)
        {
            spawnBox.PressAButtonOnce();
            return;
        }

        if (isPlayerOne || isPlayerTwo || isPlayerThree)
        {
            if (IsSpacePressed && isGrounded)
            {
                rb.AddForce(Vector3.up * JumpHeight, ForceMode.Impulse);
                //isGrounded = false;
                //Physics.gravity = new Vector3(0, -9.8f, 0);
            }

            if (!isGrounded)
            {
                IsSpacePressed = false;
                //Physics.gravity = new Vector3(0, -50, 0);
            }

            if (!isGrounded && i == 0)
            {

                speed = speed / 4;
                i++;
            }

            if (isGrounded && i == 1)
            {

                speed = speed * 4;
                i--;
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Conveyor" || col.gameObject.tag == "Doos")
        {
            isGrounded = true;
        }
        if (col.gameObject.tag == "Vuur")
        {
            if (isPlayerOne)
            {
                MinigameManager.instance.CheckIfAlive(1);
                rb.MovePosition(new Vector3(-19, 30, 17));
            }
            else if (isPlayerTwo)
            {
                //gameObject.SetActive(false);
                MinigameManager.instance.CheckIfAlive(2);
                rb.MovePosition(new Vector3(-19, 30, 19));
            }
            else if (isPlayerThree)
            {
                //gameObject.SetActive(false);
                MinigameManager.instance.CheckIfAlive(3);
                rb.MovePosition(new Vector3(-19, 30, 15));
            }
        }
    }
    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Conveyor" || col.gameObject.tag == "Doos")
        {
            isGrounded = false;
        }
    }

    private void OnDisable()
    {
        if (GamePlayerInformation != null)
        {
            GamePlayerInformation.PlayerInput.OnVariableChangeInputA -= AbuttonPressed;
        }
    }

    private void OnDestroy()
    {
        if (GamePlayerInformation != null)
        {
            GamePlayerInformation.PlayerInput.OnVariableChangeInputA -= AbuttonPressed;
        }
    }




}
