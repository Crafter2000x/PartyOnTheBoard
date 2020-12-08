using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerMenuInputManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private PlayerParty Player;
    [SerializeField] private LobbyMenuHandler LobbyManager;



    private void Start()
    {
        if (SceneManager.GetActiveScene().name.ToString() == "MainMenu")
        {
            Player = GetComponent<PlayerParty>();
            LobbyManager = GameObject.Find("LobbyCanvas").GetComponent<LobbyMenuHandler>();
            Player.OnVariableChangeInputA += ReadyButtonHandler;
            Player.OnVariableChangeInputB += ReadyButtonHandler1;
        }
        else
        {
            Debug.Log(SceneManager.GetActiveScene().name.ToString()); 
        }
    }

    private void ReadyButtonHandler(bool newVal) 
    {
        Debug.Log("Button A pressed");
        Player.Ready = true;
        LobbyManager.StartCoroutine("UpdateLobbyUI");
    }

    private void ReadyButtonHandler1(bool newVal)
    {
        Debug.Log("Button B pressed");
        Player.Ready = false;
        LobbyManager.StartCoroutine("UpdateLobbyUI");

    }


}
