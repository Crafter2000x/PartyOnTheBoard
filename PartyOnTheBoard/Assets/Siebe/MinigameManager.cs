using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MinigameManager : MonoBehaviour
{
    public static MinigameManager instance;

    private Text aliveText1;
    private Text aliveText2;
    private Text aliveText3;
    private Text timerText;
    private Text uitlegText;
    private Button startButton;
    private Text countDownText;
    public Text winnerText;
    private Text demoText;

    int i = 0;

    [SerializeField] private GameObject smoke;
    [SerializeField] public Camera MainCam;
    [SerializeField] public Camera RankCam1;
    [SerializeField] public Camera RankCam2;
    [SerializeField] private Camera uitlegCam;
    [SerializeField] List<Player> Players;
    private GameBoardManager GameBoardManager;

    float timeLeft = 90.0f;
    public float countDown = 3.0f;

    public bool gameOver = false;
    private bool clicked = false;

    public List<string> DeadPlayers = new List<string>();



    private void Start()
    {
        GameBoardManager = GameObject.Find("GameBoardManager").GetComponent<GameBoardManager>();
        int counter = 0;
        foreach (var Player in GameBoardManager.PlayerList)
        {
            Players[counter].GamePlayerInformation = Player.GetComponent<GamePlayerInformation>();
            counter++;
        }
            
    }
    void Awake()
    {
        MakeInstance();

        aliveText1 = GameObject.Find("AliveText1").GetComponent<Text>();
        aliveText2 = GameObject.Find("AliveText2").GetComponent<Text>();
        aliveText3 = GameObject.Find("AliveText3").GetComponent<Text>();
        timerText = GameObject.Find("TimerText").GetComponent<Text>();
        countDownText = GameObject.Find("CountDownText").GetComponent<Text>();
        winnerText = GameObject.Find("WinnerText").GetComponent<Text>();
        uitlegText = GameObject.Find("UitlegText").GetComponent<Text>();
        startButton = GameObject.Find("StartButton").GetComponent<Button>();
        demoText = GameObject.Find("DemoText").GetComponent<Text>();

        MainCam.enabled = true;
        RankCam1.enabled = false;
        RankCam2.enabled = false;
        winnerText.enabled = false;
        aliveText1.enabled = false;
        aliveText2.enabled = false;
        aliveText3.enabled = false;

        startButton.onClick.AddListener(Clicked);
    }

    void Update()
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
        if (clicked)
        {
            Counters();
            MainCam.enabled = true;
            uitlegCam.enabled = false;
            uitlegText.enabled = false;
            startButton.enabled = false;
            startButton.gameObject.SetActive(false);
            aliveText1.enabled = true;
            aliveText2.enabled = true;
            aliveText3.enabled = true;
            demoText.enabled = false;
        }

        CheckIfGameIsOver();
        GameOver();
    }

    private void Clicked()
    {
        clicked = true;
    }
   

    public void CheckIfAlive(int number)
    {
        if (number == 1)
        {
            aliveText1.text = "Player 1: Eliminated";
            DeadPlayers.Add("player1");
        }
        else if (number == 2)
        {
            aliveText2.text = "Player 2: Eliminated";
            DeadPlayers.Add("player2");
        }
        else if (number == 3)
        {
            aliveText3.text = "Player 3: Eliminated";
            DeadPlayers.Add("player3");
        }
    }

    void CheckIfGameIsOver()
    {
        if (DeadPlayers.Count == 3)
        {
            gameOver = true;
        }
        if (timeLeft < 0)
        {
            gameOver = true;
        }
    }

    void GameOver()
    {
        if (!gameOver)
        {

        }
        else
        {      
            timerText.enabled = false;
            aliveText1.enabled = false;
            aliveText2.enabled = false;
            aliveText3.enabled = false;
            StartCoroutine("DeleteAll");
        }
    }

    IEnumerator DeleteAll()
    {
        yield return new WaitForSeconds(10);
        foreach (GameObject item in FindObjectsOfType(typeof(GameObject)))
        {
            if (item.GetComponent<DoosCollide>() != null)
            {
                Destroy(item.gameObject);
            }
           
        }
        
        Destroy(gameObject.transform.parent.gameObject);
    }

    void Counters()
    {
        countDownText.text = Convert.ToString(Math.Round(countDown, 0));
        countDown -= Time.deltaTime;
        if (countDown < 0.5)
        {
            countDownText.color = Color.green;
            countDownText.text = "Go!";
        }
        if (countDown < 0)
        {
            countDownText.text = "";
            timerText.text = Convert.ToString(timeLeft);
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                timerText.text = "";
            }
        }
    }
    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    
}
