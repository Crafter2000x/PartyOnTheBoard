using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayerInformation : MonoBehaviour
{
    [Header("Player Informatio")]
    public string PlayerName;
    public int FirstRoll = 0;
    public int CurrentRoll = 0;
    public bool MyTurn = false;
    [Header("Network Information")]
    public GameObject PlayerNetworkClone;
    public PlayerParty PlayerInput;

    private void Start()
    {
        PlayerInput = PlayerNetworkClone.GetComponent<PlayerParty>();
        PlayerName = PlayerInput.PlayerName;
    }
}
