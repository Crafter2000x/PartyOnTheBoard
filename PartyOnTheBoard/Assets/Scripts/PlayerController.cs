//using System.Collections;
//using System.Collections.Generic;
//using System;
//using TMPro;
//using UnityEngine.UI;
//using UnityEngine;
//using Mirror;

//public class PlayerController : NetworkBehaviour
//{
//    [Header("UI")]
//    [SerializeField] private Canvas LobbyCanvas;
//    [SerializeField] private TMP_Text[] PlayerNameText = new TMP_Text[4];
//    [SerializeField] private TMP_Text[] PlayerReadyText = new TMP_Text[4];
//    [SerializeField] private  Button StartGameButton;

//    [SyncVar(hook = nameof(HandleDisplayNameChanged))]
//    public string DisplayName = "Loading...";
//    [SyncVar(hook = nameof(HandleReadyStatusChanged))]
//    public bool IsReady = false;

//    private NetworkManagerParty room;
//    private NetworkManagerParty Room 
//    {
//        get 
//        {
//            if (room != null )
//            {
//                return room;
//            }
//            return room = NetworkManager.singleton as NetworkManagerParty;        
//        }

//    }

//    public override void OnStartAuthority()
//    {
//        CmdSetDisplayName("Peter");
//        LobbyCanvas.gameObject.SetActive(true);
//    }

//    public override void OnStartClient()
//    {
//        Room.RoomPlayers.Add(this);
//        UpdateDisplay();
//    }

//    public override void OnStopClient()
//    {
//        Room.RoomPlayers.Remove(this);
//        UpdateDisplay();
//    }

//    public void HandleDisplayNameChanged(String oldValue, String newValue) => UpdateDisplay();
//    public void HandleReadyStatusChanged(Boolean oldValue, Boolean newValue) => UpdateDisplay();

//    private void UpdateDisplay() 
//    {
//        if (!hasAuthority)
//        {
//            foreach (var player in Room.RoomPlayers)
//            {
//                if (player.hasAuthority)
//                {
//                    player.UpdateDisplay();
//                    break;
//                }
//            }
//            return;
//        }

//        for (int i = 0; i < PlayerNameText.Length; i++)
//        {
//            PlayerNameText[i].text = "Waiting for Player...";
//            PlayerReadyText[i].text = string.Empty;
//        }

//        for (int i = 0; i < Room.RoomPlayers.Count; i++)
//        {
//            PlayerNameText[i].text = Room.RoomPlayers[i].DisplayName;
//            PlayerReadyText[i].text = Room.RoomPlayers[i].IsReady ?
//                "<Color=green>Ready</Color>" :
//                "<Color=red>Ready</Color>";
//        }
//    }

//    public void HandleReadyToStart(bool ReadyToStart)
//    {
//        StartGameButton.interactable = ReadyToStart;
//    }

//    [Command]
//    private void CmdSetDisplayName(string displayname) 
//    {
//        DisplayName = displayname;
//    }

//    [Command]
//    public void CmdReadyUp() 
//    {
//        IsReady = !IsReady;
//       // Room.NotifyPlayersOfReadyState();
//    }

//    [Command]
//    public void CmdStartGame() 
//    {
//        if (Room.RoomPlayers[0].connectionToClient != connectionToClient) 
//        {
//            return;
//        }

//        Debug.Log("Game has started");
//    }
//}

