using System;
using System.Linq;
using Mirror;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class LobbyMobileConnectHandler : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private NetworkManagerParty NetworkManager;
    [SerializeField] private PlayerInformation PlayerInfo;

    [Header("UI")]
    [SerializeField] private TMP_InputField NameInputField;
    [SerializeField] private TMP_InputField IpAddressInputField;
    [SerializeField] private Canvas MainMenuCanvas;
    [SerializeField] private Button ConnectingButton;
    [SerializeField] private Button ReturnButton;

    public void ExitToMain() 
    {
        MainMenuCanvas.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        NetworkManagerParty.OnClientConnected += HandleClientConnected;
        NetworkManagerParty.OnClientDisconnected += HandleClientDisconnected;

        if (PlayerInfo.LastUsedName != string.Empty)
        {
            NameInputField.text = PlayerInfo.LastUsedName;
        }
    }

    private void OnDisable()
    {
        NetworkManagerParty.OnClientConnected -= HandleClientConnected;
        NetworkManagerParty.OnClientDisconnected -= HandleClientDisconnected;

        PlayerInfo.LastUsedName = NameInputField.text;
        NameInputField.text = string.Empty;
        IpAddressInputField.text = string.Empty;
    }

    private void HandleClientConnected() 
    {
        ConnectingButton.interactable = true;
        ReturnButton.interactable = true;
        gameObject.SetActive(false);

    }    
    
    private void HandleClientDisconnected() 
    {
        ConnectingButton.interactable = true;
        ReturnButton.interactable = true;
    }

    public void JoinLobby() 
    {
        string IpAddress = IpAddressInputField.text;
        NetworkManager.networkAddress = IpAddress;

        NetworkManager.StartClient();
        ConnectingButton.interactable = false;
        ReturnButton.interactable = false;
    }

}
