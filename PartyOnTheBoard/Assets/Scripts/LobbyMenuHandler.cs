using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class LobbyMenuHandler : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private NetworkManagerParty NetworkManager;

    [Header("UI")]
    [SerializeField] private Canvas MainMenuCanvas;
    [SerializeField] private Image[] PlayerPanels;
    [SerializeField] private TMP_Text[] PlayerText;

    private void OnEnable()
    {

        NetworkManagerParty.OnPlayerObjectCreated += OnPlayerJoinLobby;
        NetworkManagerParty.OnPlayerObjectRemoved += OnPlayerLeaveLobby;
        HostGame();
    }

    private void OnDisable()
    {
        NetworkManagerParty.OnPlayerObjectCreated -= OnPlayerJoinLobby;
        NetworkManagerParty.OnPlayerObjectRemoved -= OnPlayerLeaveLobby;
    }

    public void HostGame()
    {
        NetworkManager.StartServer();
    }

    public void ExitLobby()
    {
        NetworkManager.StopServer();
        MainMenuCanvas.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    private void OnPlayerJoinLobby()
    {
        StartCoroutine("UpdateLobbyUI");
    }
    private void OnPlayerLeaveLobby()
    {
        StartCoroutine("UpdateLobbyUI");
    }




    IEnumerator UpdateLobbyUI()
    {
        yield return new WaitForSeconds(.1f);

        foreach (Image panel in PlayerPanels)
        {
            panel.color = Color.white;
        }

        foreach (TMP_Text text in PlayerText)
        {
            text.text = "Waiting for connection...";
            text.color = Color.white;
        }

        int counter = new int();

        foreach (var player in NetworkManager.PartyPlayers)
        {
            PlayerPanels[counter].color = Color.red;
            PlayerText[counter].text = $"{player.GetComponent<PlayerParty>().PlayerName} Connected";
            PlayerText[counter].color = Color.black;
            counter++;
        }
    }
}
