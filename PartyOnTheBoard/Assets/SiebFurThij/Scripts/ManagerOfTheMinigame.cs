using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Linq;
using UnityEngine.UI;
using System;

public class ManagerOfTheMinigame : MonoBehaviour
{
    #region Variables
    [SerializeField] public Camera mainCam;
    [SerializeField] public Camera startCam;
    [SerializeField] public Camera endCam;
    [SerializeField] private List<Transform> DoorPlaces;
    [SerializeField] private List<Transform> PlayerPlaces;
    [SerializeField] private List<Transform> RankingPlaces;
    [SerializeField] public List<GameObject> Doors;
    [SerializeField] public List<GameObject> Players;
    [SerializeField] public List<Material> PlayerColors;
    [SerializeField] public List<Material> DoorColors;
    [SerializeField] private List<string> EngWrd;
    [SerializeField] private List<string> NlWrd;
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject player;
    [SerializeField] private List<int> NLplaces;
    [SerializeField] private List<int> Scores;
    [SerializeField] private TextMeshProUGUI Question;
    [SerializeField] public TextMeshProUGUI ScoreP1;
    [SerializeField] public TextMeshProUGUI ScoreP2;
    [SerializeField] public TextMeshProUGUI ScoreP3;
    [SerializeField] public TextMeshProUGUI ScoreP4;
    [SerializeField] public TextMeshProUGUI InvText;
    [SerializeField] private TextMeshProUGUI uitleg;
    [SerializeField] private TextMeshProUGUI demoText;
    [SerializeField] private TextMeshProUGUI FirstPlaceText;
    [SerializeField] private TextMeshProUGUI SecondPlaceText;
    [SerializeField] private TextMeshProUGUI ThirdPlaceText;
    [SerializeField] private TextMeshProUGUI FourthPlaceText;
    [SerializeField] private Button startButton;
    [SerializeField] private GameBoardManager GameBoardManager;
    int i = 0;
    int k = 0;
    public List<GameObject> PlayersWhoPassedTrue;
    public List<GameObject> PlayersWhoPassedFalse;
    [SerializeField] int AmountOfRounds;
    private string DoorText;
    public bool GameOver = false;
    #endregion
    #region Start/Update
    void Start()
    {
        GameBoardManager = GameObject.Find("GameBoardManager").GetComponent<GameBoardManager>();

        EnableAndDisableThings();
        InstantiateObjects();
        GenerateWords();
        startButton.onClick.AddListener(Clicked);
    }
    void FixedUpdate()
    {
        DemoText();
        GameIsOver();
    }
    #endregion
    #region GamePreparations
    void DemoText()
    {
        while (i == 0)
        {
            StartCoroutine(Delay());
            i++;
        }
        IEnumerator Delay()
        {
            i++;
            demoText.color = Color.yellow;
            yield return new WaitForSeconds(0.5f);
            demoText.color = Color.black;
            yield return new WaitForSeconds(0.5f);
            i = 0;
        }
    }
    void Clicked()
    {
        mainCam.enabled = true;
        startCam.enabled = false;
        endCam.enabled = false;
        Question.enabled = true;
        uitleg.enabled = false;
        startButton.gameObject.SetActive(false);
        demoText.enabled = false;
    }
    void EnableAndDisableThings()
    {
        Question.enabled = false;
        InvText.enabled = false;
        mainCam.enabled = false;
        startCam.enabled = true;
        ScoreP1.enabled = false;
        ScoreP2.enabled = false;
        ScoreP3.enabled = false;
        ScoreP4.enabled = false;
        FirstPlaceText.enabled = false;
        SecondPlaceText.enabled = false;
        ThirdPlaceText.enabled = false;
        FourthPlaceText.enabled = false;
    }
    private void InstantiateObjects()
    {
        GameObject SpawnedDoor;
        GameObject SpawnedPlayer;

        for (int i = 0; i < DoorPlaces.Count; i++)
        {
            SpawnedDoor = Instantiate(door, DoorPlaces[i]);
            SpawnedPlayer = Instantiate(player, PlayerPlaces[i]);
            Players.Add(SpawnedPlayer);
            Players[i].tag = "P" + (i + 1);
            Players[i].GetComponent<Renderer>().material = PlayerColors[i];
            SpawnedDoor.GetComponentInChildren<TextMeshProUGUI>().text = DoorText;
            Doors.Add(SpawnedDoor);
        }

        int counter = 0;
        foreach (var player in GameBoardManager.PlayerList)
        {
            Players[counter].GetComponent<PlayerDoorDash>().GamePlayerInformation = player.GetComponent<GamePlayerInformation>();
            counter++;
        }
    }
    private void GenerateWords()
    {
        EngWrd.Clear();
        NlWrd.Clear();
        NLplaces.Clear();
        int index;

        string readFromFilePath = Application.streamingAssetsPath + "/EngelsWrd" + ".txt";
        EngWrd = File.ReadAllLines(readFromFilePath).ToList();
        string readFromAnotherFilePath = Application.streamingAssetsPath + "/NederlandsWrd" + ".txt";
        NlWrd = File.ReadAllLines(readFromAnotherFilePath).ToList();

        foreach (var DoorText in Doors)
        {
            string word;
            index = UnityEngine.Random.Range(0, NlWrd.Count - 1);
            word = NlWrd[index];
            NLplaces.Add(index);
            DoorText.GetComponentInChildren<TextMeshProUGUI>().text = word;
        }

        string ChosenWord;
        int RandomIndex = UnityEngine.Random.Range(0, 4);
        ChosenWord = EngWrd[NLplaces[RandomIndex]];
        Question.text = "Which of the following words is the correct translation of: " + ChosenWord;
        InvText.text = NlWrd[NLplaces[RandomIndex]];
    }
    #endregion
    #region AfterRounds
    public void ScoreHandler(int score1, int score2, int score3, int score4)
    {
        ScoreP1.text = score1.ToString();
        ScoreP2.text = score2.ToString();
        ScoreP3.text = score3.ToString();
        ScoreP4.text = score4.ToString();
    }
    public void AfterRound()
    {
        ScoreP1.enabled = true;
        ScoreP2.enabled = true;
        ScoreP3.enabled = true;
        ScoreP4.enabled = true;
        ChangeDoorColors();
        AmountOfRounds--;
        if (AmountOfRounds <= 0)
        {
            StartCoroutine(DelayBeforeGameOver());
        }
        if (!GameOver)
        {
            StartCoroutine(StartNewRound());
        }
    }
    void ChangeDoorColors()
    {
        for (int i = 0; i < Doors.Count; i++)
        {
            if (Doors[i].GetComponentInChildren<TextMeshProUGUI>().text == InvText.text)
            {
                Doors[i].GetComponent<Renderer>().material = DoorColors[0];
            }
            else
            {
                Doors[i].GetComponent<Renderer>().material = DoorColors[1];
            }
        }
    }
    void TeleportPlayers()
    {
        for (int i = 0; i < Players.Count; i++)
        {
            Players[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            Players[i].GetComponent<Rigidbody>().position = PlayerPlaces[i].position;
        }
    }
    void NewRound()
    {
        for (int i = 0; i < Doors.Count; i++)
        {
            Doors[i].GetComponent<Renderer>().material = DoorColors[2];
        }
        TeleportPlayers();
        GenerateWords();
    }
    IEnumerator StartNewRound()
    {
        yield return new WaitForSeconds(5);
        NewRound();
    }
    #endregion
    #region GameEnding
    void GameIsOver()
    {
        if (GameOver)
        {
            endCam.enabled = true;
            mainCam.enabled = false;
            Question.enabled = false;
            ScoreP1.enabled = false;
            ScoreP2.enabled = false;
            ScoreP3.enabled = false;
            ScoreP4.enabled = false;
            if (k == 0)
            {
                AddScoresToList();
                TeleportToRanking();
                SetScoresInText();
                k++;
                StartCoroutine("DestroyAll");
            }
        }
    }

    IEnumerator DestroyAll() 
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject.transform.parent.transform.parent.gameObject);
    }


    IEnumerator DelayBeforeGameOver()
    {
        yield return new WaitForSeconds(5);
        GameOver = true;
    }
    void AddScoresToList()
    {
        int score1 = Convert.ToInt32(ScoreP1.text);
        Scores.Add(score1);
        int score2 = Convert.ToInt32(ScoreP2.text);
        Scores.Add(score2);
        int score3 = Convert.ToInt32(ScoreP3.text);
        Scores.Add(score3);
        int score4 = Convert.ToInt32(ScoreP4.text);
        Scores.Add(score4);
        Scores.Sort((a, b) => b.CompareTo(a));
    }
    void TeleportToRanking()
    {
        int a = 0;
        int b = 0;
        int c = 0;
        int d = 0;
        for (int i = 0; i < Players.Count; i++)
        {
            if (Convert.ToInt32(ScoreP1.text) == Scores[i] && a == 0)
            {
                Players[0].GetComponent<Rigidbody>().velocity = Vector3.zero;
                Players[0].GetComponent<Rigidbody>().position = RankingPlaces[i].position;
                a++;
            }
            else if (Convert.ToInt32(ScoreP2.text) == Scores[i] && b == 0)
            {
                Players[1].GetComponent<Rigidbody>().velocity = Vector3.zero;
                Players[1].GetComponent<Rigidbody>().position = RankingPlaces[i].position;
                b++;
            }
            else if (Convert.ToInt32(ScoreP3.text) == Scores[i] && c == 0)
            {
                Players[2].GetComponent<Rigidbody>().velocity = Vector3.zero;
                Players[2].GetComponent<Rigidbody>().position = RankingPlaces[i].position;
                c++;
            }
            else if (Convert.ToInt32(ScoreP4.text) == Scores[i] & d == 0)
            {
                Players[3].GetComponent<Rigidbody>().velocity = Vector3.zero;
                Players[3].GetComponent<Rigidbody>().position = RankingPlaces[i].position;
                d++;
            }
        }
    }
    void SetScoresInText()
    {
        FirstPlaceText.text = "Score: " + Convert.ToString(Scores[0]);
        SecondPlaceText.text = "Score: " + Convert.ToString(Scores[1]);
        ThirdPlaceText.text = "Score: " + Convert.ToString(Scores[2]);
        FourthPlaceText.text = "Score: " + Convert.ToString(Scores[3]);
        FirstPlaceText.enabled = true;
        SecondPlaceText.enabled = true;
        ThirdPlaceText.enabled = true;
        FourthPlaceText.enabled = true;
    }
    #endregion
}
