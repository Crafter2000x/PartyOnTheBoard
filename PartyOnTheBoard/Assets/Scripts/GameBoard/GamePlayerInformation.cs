using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayerInformation : MonoBehaviour
{
    [Header("Player Information")]
    public string PlayerName;
    public int FirstRoll = 0;
    public int CurrentRoll = 0;
    public bool PlayerDoneMoving = true;
    public PlayerObjectMovmentScript BoardObject;
    [Header("Network Information")]
    public GameObject PlayerNetworkClone;
    public PlayerParty PlayerInput;

    private void Start()
    {
        PlayerInput = PlayerNetworkClone.GetComponent<PlayerParty>();
        PlayerName = PlayerInput.PlayerName;
    }
}
