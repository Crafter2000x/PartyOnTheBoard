using System.Net;
using System.Net.Sockets;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LobbyMenuHandler : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private NetworkManagerParty NetworkManager;

    [Header("UI")]
    [SerializeField] private Canvas MainMenuCanvas;
    [SerializeField] private Image[] PlayerPanels;
    [SerializeField] private TMP_Text[] PlayerText;
    [SerializeField] private Image ReadyPanel;
    [SerializeField] private TMP_Text ReadyText;
    [SerializeField] private TMP_Text IpAdress;

    [Header("Lobby")]
    [SerializeField] private int MinimumPlayers;
    [SerializeField] private int AmountReady;

    private bool CountDownIsRunnig;
    private Color PlayerPanelColor;
    private void Start()
    {
        PlayerPanelColor = PlayerPanels[0].color;
    }

    private void OnEnable()
    {

        NetworkManagerParty.OnPlayerJoinServer += OnPlayerJoinLobby;
        NetworkManagerParty.OnPlayerDisconnectServer += OnPlayerLeaveLobby;
        HostGame();
    }

    private void OnDisable()
    {
        NetworkManagerParty.OnPlayerJoinServer -= OnPlayerJoinLobby;
        NetworkManagerParty.OnPlayerDisconnectServer -= OnPlayerLeaveLobby;
    }

    public void HostGame()
    {
        NetworkManager.StartServer();
        IpAdress.text = LocalIPAddress();
    }

    public string LocalIPAddress()
    {
        IPHostEntry host;
        string localIP = "";
        host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                localIP = ip.ToString();
                break;
            }
        }
        return localIP;
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

        yield return new WaitForSeconds(.3f);

        AmountReady = 0;

        foreach (Image panel in PlayerPanels)
        {
            panel.color = PlayerPanelColor;
        }

        foreach (TMP_Text text in PlayerText)
        {
            text.text = "Waiting for connection...";
            text.color = Color.white;
        }

        int counter = new int();

        foreach (var player in NetworkManager.PartyPlayers)
        {
            if (player.GetComponent<PlayerParty>().Ready)
            {
                PlayerPanels[counter].color = Color.green;
                AmountReady++;
            }
            else
            {
                PlayerPanels[counter].color = Color.red;
            }
            PlayerText[counter].text = $"{player.GetComponent<PlayerParty>().PlayerName} Connected";
            PlayerText[counter].color = Color.black;
            counter++;
        }

        if (CountDownIsRunnig == true)
        {
            StopCoroutine("StartCountDown");
            CountDownIsRunnig = false;
            ReadyText.text = "0";
            ReadyPanel.gameObject.SetActive(false);
        }

        if (AmountReady >= MinimumPlayers)
        {
            StartCoroutine("StartCountDown");
        }
    }

    IEnumerator StartCountDown() 
    {
        CountDownIsRunnig = true;
        ReadyPanel.gameObject.SetActive(true);

        ReadyText.text = "5";
        yield return new WaitForSeconds(1);
        ReadyText.text = "4";
        yield return new WaitForSeconds(1);
        ReadyText.text = "3";
        yield return new WaitForSeconds(1);
        ReadyText.text = "2";
        yield return new WaitForSeconds(1);
        ReadyText.text = "1";
        yield return new WaitForSeconds(1);
        ReadyText.text = "0";
        yield return new WaitForSeconds(1);
        Debug.Log("Start Game");

        foreach (var Player in NetworkManager.PartyPlayers)
        {
            Destroy(Player.GetComponent<PlayerMenuInputManager>());
        }

        NetworkManager.InProgress = true;
        SceneManager.LoadScene("GameBoardOne");
    }
}
