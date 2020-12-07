using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror; 

public class PlayerParty : NetworkBehaviour
{
    [Header("Player input")]
    public float HorizontalInput;
    public float VerticalInput;

    [Command]
    private void HandleInput() 
    {
        if (isLocalPlayer)
        {
            HorizontalInput = Input.GetAxis("Horizontal");
            VerticalInput = Input.GetAxis("Vertical");
        }
    }

    private void Update()
    {
        HandleInput();
    }

}
