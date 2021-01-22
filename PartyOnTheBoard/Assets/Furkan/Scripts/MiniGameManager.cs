using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager Instance;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        beginCameraAudioLis = beginCamera.GetComponent<AudioListener>();
        mainCameraAudioLis = mainCamera.GetComponent<AudioListener>();
        resultCameraAudioLis = resultCamera.GetComponent<AudioListener>();

        AssignPlayers();
        CameraPositionChange(0);
    }

    int number = 1;

    // Update is called once per frame
    public void Update()
    {
        if (textBoxCounter.enabled && (Time.time >= timeWhenDisappear))
        {
            textBoxCounter.enabled = false;
        }

        if (timerActive)
        {
            if (number == 1)
            {
                Counter();
            }
            else if (number == 2)
            {
                TimerHandler();
                ScoreHandler();
                GameStart();
            }
            else if (number == 3)
            {
                WinnersAndLosers();
            }
        }
    }

    public GameObject beginCamera;
    public GameObject mainCamera;
    public GameObject resultCamera;

    AudioListener beginCameraAudioLis;
    AudioListener mainCameraAudioLis;
    AudioListener resultCameraAudioLis;

    public void CameraPositionChange(int camPosition)
    {
        if (camPosition == 0)
        {
            beginCamera.SetActive(true);
            beginCameraAudioLis.enabled = true;

            mainCameraAudioLis.enabled = false;
            mainCamera.SetActive(false);

            resultCameraAudioLis.enabled = false;
            resultCamera.SetActive(false);
        }
        if (camPosition == 1)
        {
            mainCamera.SetActive(true);
            mainCameraAudioLis.enabled = true;

            beginCameraAudioLis.enabled = false;
            beginCamera.SetActive(false);

            resultCameraAudioLis.enabled = false;
            resultCamera.SetActive(false);

        }
        if (camPosition == 2)
        {
            resultCamera.SetActive(true);
            resultCameraAudioLis.enabled = true;

            beginCameraAudioLis.enabled = false;
            beginCamera.SetActive(false);

            mainCameraAudioLis.enabled = false;
            mainCamera.SetActive(false);
        }
    }

    private float startCounter = 3;
    public Text textBoxCounter;
    private float timeToAppear = 1;
    private float timeWhenDisappear = 0;

    public void Counter()
    {
        textBoxCounter.text = startCounter.ToString();
        startCounter -= Time.deltaTime;
        textBoxCounter.text = Mathf.Round(startCounter).ToString();

        if (startCounter <= 0)
        {
            textBoxCounter.text = "Go!";
            number = 2;
        }

        textBoxCounter.enabled = true;
        timeWhenDisappear = Time.time + timeToAppear;
    }

    public List<PlayerMovement> players;
    public Text textBoxScore1;
    public Text textBoxScore2;
    int team1;
    int team2;

    public void ScoreHandler()
    {
        team1 = players[0].score + players[1].score;
        team2 = players[2].score + players[3].score;

        textBoxScore1.text = "Score Team 1: " + Convert.ToString(team1);
        textBoxScore2.text = "Score Team 2: " + Convert.ToString(team2);
    }

    private float timeStart = 90;
    public Text textBoxTimer;
    bool timerActive = false;

    public void TimerHandler()
    {
        textBoxTimer.text = timeStart.ToString();
        timeStart -= Time.deltaTime;
        textBoxTimer.text = "Time: " + Mathf.Round(timeStart).ToString();

        if (timeStart <= 0)
        {
            textBoxTimer.text = "Time: 0";

            CameraPositionChange(2);

            number = 3;
        }
    }

    public void IsTimeActive()
    {
        timerActive = !timerActive;
    }

    public bool gameStarted = false;

    public void GameStart()
    {
        gameStarted = true;
    }

    public bool team1Win = false;
    public bool team2Win = false;

    public void WinnersAndLosers()
    {
        if (team1 > team2)
        {
            team1Win = true;
        }
        else if (team1 < team2)
        {
            team2Win = true;
        }

        StartCoroutine("DeleteAll");
    }

    IEnumerator DeleteAll() 
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject.transform.parent.gameObject);
    }

    private GameBoardManager GameBoardManager;
    private void AssignPlayers() 
    {
        int counter = 0;
        GameBoardManager = GameObject.Find("GameBoardManager").GetComponent<GameBoardManager>();

        foreach (var Player in GameBoardManager.PlayerList)
        {
            players[counter].MainPlayerInfo = Player.GetComponent<GamePlayerInformation>();
            counter++;
        }
    
    
    }
}
