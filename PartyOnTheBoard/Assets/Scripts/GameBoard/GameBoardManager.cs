using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameBoardManager : MonoBehaviour
{
    [Header("Networking")]
    public NetworkManagerParty NetworkManager;
    [Header("Player Managment")]
    public List<GameObject> PlayerList;
    [Header("Minigames")]
    public List<GameObject> MiniGames;
    [Header("Object Reference")]
    [SerializeField] private GameObject GameBoardPlayer;
    [SerializeField] private GameObject MessageBox;
    [SerializeField] private GameObject DiceRollObject;
    [SerializeField] private GameObject DiceRollCanvas;
    [SerializeField] private Transform[] DiceRollLocations;
    [SerializeField] private Transform PlayersObject;
    [SerializeField] private Transform SceneObject;
    [SerializeField] private Transform MiniGamesObject;

    public enum GamesStates 
    {
        GameSetup,
        TurnLoop,
        MiniGame,
    }

    public GamesStates GameState;

    private void Start()
    {
        GameState = GamesStates.GameSetup;
        GameLoop();
    }

    private void GameLoop()
    {
        switch (GameState)
        {
            case GamesStates.GameSetup:
                GameSetup();
                break;
            case GamesStates.TurnLoop:
                TurnLoop();
                break;
            case GamesStates.MiniGame:
                MiniGame();
                break;
        }
    }

    #region Game Setup
    private void GameSetup() 
    {
        NetworkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManagerParty>();

        GameObject SpawndObject;
        foreach (var Player in NetworkManager.PartyPlayers)
        {
            SpawndObject = Instantiate(GameBoardPlayer);
            SpawndObject.GetComponent<GamePlayerInformation>().PlayerNetworkClone = Player;
            PlayerList.Add(SpawndObject);
        }

        StartCoroutine("DecideTurnOrder");
        // Decide the order in what players go
    }

    IEnumerator DecideTurnOrder() 
    {
        DisplayMessage("Roll for the turn order!", 3);

        yield return new WaitUntil(MessageBoxEnabled);

        GameObject SpawndObject;
        GameObject[] SpawedObjects = new GameObject[4];
        int Counter = 0;

        DiceRollCanvas.SetActive(true);

        foreach (var Player in PlayerList)
        {
            SpawndObject = Instantiate(DiceRollObject, DiceRollLocations[Counter]);
            SpawndObject.GetComponent<NumberRollScript>().Player = Player.GetComponent<GamePlayerInformation>();
            SpawndObject.GetComponent<NumberRollScript>().FirstRoll = true;
            SpawedObjects[Counter] = SpawndObject;
            Counter++;
        }

        yield return new WaitUntil(HasEveryoneRolled);

        PlayerList.Sort(SortByScore);

        foreach (var Player in PlayerList)
        {
            Player.transform.parent = PlayersObject;
        }

        yield return new WaitForSeconds(5);

        foreach (GameObject Object in SpawedObjects)
        {
            GameObject.Destroy(Object);
        }
        Counter = 0;

        GameState = GamesStates.TurnLoop;
        GameLoop();
        // Now start the main loop of player getting a turn
    }
    static int SortByScore(GameObject p1, GameObject p2)
    {
        return p2.GetComponent<GamePlayerInformation>().FirstRoll.CompareTo(p1.GetComponent<GamePlayerInformation>().FirstRoll);
    }

    bool HasEveryoneRolled() 
    {  
        foreach (var player in PlayerList)
        {
            if (player.GetComponent<GamePlayerInformation>().FirstRoll == 0)
            {
                return false;
            }
        }
        return true;
    }
    #endregion

    #region Turn loop
    private GamePlayerInformation PlayerInfo;
    private void TurnLoop() 
    {
        StartCoroutine("OneTurnLoop");
    }

    IEnumerator OneTurnLoop() 
    {
        GameObject SpawndObject;

        foreach (var Player in PlayerList)
        {

            PlayerInfo = Player.GetComponent<GamePlayerInformation>();
            DisplayMessage($"{PlayerInfo.PlayerName} his turn!", 2);

            yield return new WaitUntil(MessageBoxEnabled);

            SpawndObject = Instantiate(DiceRollObject, DiceRollLocations[4]);
            SpawndObject.GetComponent<NumberRollScript>().Player = Player.GetComponent<GamePlayerInformation>();

            yield return new WaitUntil(HasPlayerRolled);

            yield return new WaitForSeconds(3);

            GameObject.Destroy(SpawndObject.gameObject);
            SpawndObject = null;

            yield return new WaitForSeconds(6);
            PlayerInfo.CurrentRoll = 0;
            PlayerInfo = null;
        }

        GameState = GamesStates.MiniGame;
        GameLoop();
    }

    bool HasPlayerRolled() 
    {
        if (PlayerInfo.CurrentRoll == 0)
        {
            return false;
        }

        return true;
    }
    #endregion

    #region Minigames
    private void MiniGame() 
    {
        DisplayMessage("Minigame time!!", 5);
        StartCoroutine("MiniGameStart");
    }

    IEnumerator MiniGameStart() 
    {
        int RandomInt;
        GameObject SpanwedObject;

        yield return new WaitUntil(MessageBoxEnabled);

        RandomInt = Random.Range(0, MiniGames.Count);

        SceneObject.gameObject.SetActive(false);
        SpanwedObject = Instantiate(MiniGames[RandomInt], MiniGamesObject);

        yield return new WaitUntil(MiniGameFinished);

    }

    bool MiniGameFinished() 
    {
        return false;
    }

    #endregion

    #region Error Handeling
    float Duration;
    bool OneDisconnect;
    private void NetworkDisconnectHandler() 
    {
        if (OneDisconnect){ return; }
        OneDisconnect = true;
        StartCoroutine("ReturnToMainMenu");
        DisplayMessage("a fatal error has occurred connection is terminated, returning to the main menu", 7);
    }
    private void DisplayMessage(string Message, float fDuration)
    {
        MessageBox.SetActive(true);
        MessageBox.GetComponentInChildren<TMP_Text>().text = Message;
        Duration = fDuration;
        StartCoroutine("DisableMessage");
    }
    bool MessageBoxEnabled() 
    {
        if (MessageBox.activeSelf == true)
        {
            return false;
        }
        return true;
    }
    IEnumerator DisableMessage() 
    {
        yield return new WaitForSeconds(Duration);
        MessageBox.GetComponentInChildren<TMP_Text>().text = "";
        MessageBox.SetActive(false);
    }
    IEnumerator ReturnToMainMenu() 
    {
        NetworkManager.StopServer();
        Destroy(NetworkManager.gameObject);
        Debug.Log("Manager killed");
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("MainMenu");
    }
    #endregion

    #region Action Listening
    private void OnEnable()
    {
        NetworkManagerParty.OnPlayerDisconnectServer += NetworkDisconnectHandler;
    }

    private void OnDisable()
    {
        NetworkManagerParty.OnPlayerDisconnectServer -= NetworkDisconnectHandler;
    }
    #endregion
}
