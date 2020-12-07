using TMPro;
using UnityEngine.UI;
using UnityEngine;
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
        UpdateLobbyUI();
    }
    private void OnPlayerLeaveLobby()
    {
        UpdateLobbyUI();
    }

    private void UpdateLobbyUI()
    {
        foreach (Image panel in PlayerPanels)
        {
            panel.color = Color.white;
        }

        foreach (TMP_Text text in PlayerText)
        {
            text.text = "Waiting for connection...";
            text.color = Color.white;
        }

        Debug.Log($"Updating the UI {NetworkManager.PartyPlayers.Count}");
        int counter = new int();

        foreach (var player in NetworkManager.PartyPlayers)
        {
            PlayerPanels[counter].color = Color.red;
            PlayerText[counter].text = "Player Connected";
            PlayerText[counter].color = Color.black;
            counter++;
        }
    }

}
