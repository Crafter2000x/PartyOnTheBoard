using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerParty : NetworkBehaviour
{
    public  PlayerInformation PlayerInfo;
    [Header("PlayerInformation")]
    public string PlayerName;
    public bool Ready;
    [Header("Player input")]
    public Vector3 Movement;
    public bool InputA;
    public bool InputB;

    private void Start()
    {
        if (isLocalPlayer)
        {
            PlayerInfo = GameObject.Find("PlayerInformation").GetComponent<PlayerInformation>();
            SendUserName(PlayerInfo.LastUsedName);
        }
    }

    private Vector3 HandleMovement()
    {
        if (isLocalPlayer)
        {
            float HorizontalInput = Input.GetAxis("Horizontal");
            float VerticalInput = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(HorizontalInput, VerticalInput, 0);
            return movement;
        }
        Vector3 app = new Vector3(1, 1, 1);
        return app;
    }

    private bool HandleInputA() 
    {
        if (isLocalPlayer)
        {
            bool InputA = new bool();
            InputA = Input.GetButtonDown("Button A");
            return InputA;
        }
        return false;
    }

    private bool HandleInputB()
    {
        if (isLocalPlayer)
        {
            bool InputB = new bool();
            InputB = Input.GetButtonDown("Button B");
            return InputB;
        }
        return false;
    }

    private void Update()
    {
        if (isLocalPlayer)
        {
            SendMovement(HandleMovement());
            SendInputA(HandleInputA());
            SendIputB(HandleInputB());
        }
    }

    [Command]
    private void SendMovement(Vector3 movement)
    {
        Movement = movement;
    }

    [Command]
    private void SendInputA(bool A)
    {
        InputA = A;
        if (A == true)
        {
            Debug.Log($"Button A pressed for {gameObject.name}");
        }
    }

    [Command]
    private void SendIputB(bool B)
    {
        InputB = B;
        if (B == true)
        {
            Debug.Log($"Button B pressed for {gameObject.name}");
        }
    }

    [Command]
    private void SendUserName(string Name) 
    {
        PlayerName = Name;
    }

}
