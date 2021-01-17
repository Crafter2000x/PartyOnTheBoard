using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawn : MonoBehaviour
{
    public static BalloonSpawn Instance;
    public void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    bool alreadyStarted = true;

    public void Update()
    {
        if (MiniGameManager.Instance.gameStarted)
        {
            if (alreadyStarted)
            {
                StartCoroutine(BalloonDrop1());
                StartCoroutine(BalloonDrop2());
                StartCoroutine(BalloonDrop3());
                StartCoroutine(BalloonDrop4());

                alreadyStarted = false;
            }
        }
    }

    [SerializeField] private GameObject Balloons1; // 100 score
    public int xPosition1;
    public int zPosition1;
    public int balloonCount1;

    public IEnumerator BalloonDrop1()
    {
        while (balloonCount1 < 23)
        {
            xPosition1 = Random.Range(-50, 50);
            zPosition1 = Random.Range(50, -50);
            Instantiate(Balloons1, new Vector3(xPosition1, 6.3f, zPosition1), Quaternion.identity);
            yield return new WaitForSeconds(4f);
            balloonCount1 += 1;
        }
    }

    [SerializeField] private GameObject Balloons2;  // 300 score
    int xPosition2;
    int zPosition2;
    int balloonCount2;

    IEnumerator BalloonDrop2() 
    {
        while (balloonCount2 < 15)
        {
            xPosition2 = Random.Range(-50, 50);
            zPosition2 = Random.Range(50, -50);
            Instantiate(Balloons2, new Vector3(xPosition2, 4.5f, zPosition2), Quaternion.identity);
            yield return new WaitForSeconds(6f);
            balloonCount2 += 1;
        }
    }

    [SerializeField] private GameObject Balloons3;  //Speedboost
    int xPosition3;
    int zPosition3;
    int balloonCount3;

    IEnumerator BalloonDrop3()
    {
        while (balloonCount3 < 10) 
        {
            xPosition3 = Random.Range(-50, 50);
            zPosition3 = Random.Range(50, -50);
            Instantiate(Balloons3, new Vector3(xPosition3, 4.5f, zPosition3), Quaternion.identity);
            yield return new WaitForSeconds(9f);
            balloonCount3 += 1;
        }
    }

    [SerializeField] private GameObject Balloons4;  //Lose score and speed
    int xPosition4;
    int zPosition4;
    int balloonCount4;

    IEnumerator BalloonDrop4()
    {
        while (balloonCount4 < 13) 
        {
            xPosition4 = Random.Range(-50, 50);
            zPosition4 = Random.Range(50, -50);
            Instantiate(Balloons4, new Vector3(xPosition4, 4.5f, zPosition4), Quaternion.identity);
            yield return new WaitForSeconds(7f);
            balloonCount4 += 1;
        }
    }
}
