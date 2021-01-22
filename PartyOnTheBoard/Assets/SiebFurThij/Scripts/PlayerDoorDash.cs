using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDoorDash : MonoBehaviour
{
    #region Variables
    [SerializeField] public GameObject Go;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed = 4f;
    public GamePlayerInformation GamePlayerInformation;
    private int score1;
    private int score2;
    private int score3;
    private int score4;
    #endregion
    #region Movement
    void FixedUpdate()
    {
        if (!GameObject.Find("MinigameManager").GetComponent<ManagerOfTheMinigame>().GameOver)
        {
            Move();
        }
    }
    public void Move()
    {
        if (!GameObject.Find("MinigameManager").GetComponent<ManagerOfTheMinigame>().GameOver && GamePlayerInformation != null)
        {
            if (Go.tag == "P1")
            {
                //WASD Movement
                float horizontal = GamePlayerInformation.PlayerInput.Movement.x;
                float vertical = GamePlayerInformation.PlayerInput.Movement.y;
                Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
                rb.AddForce(direction * speed, ForceMode.Impulse);
            }
            else if (Go.tag == "P2")
            {
                //TFGH Movement
                float horizontal = GamePlayerInformation.PlayerInput.Movement.x;
                float vertical = GamePlayerInformation.PlayerInput.Movement.y;
                Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
                rb.AddForce(direction * speed, ForceMode.Impulse);
            }
            else if (Go.tag == "P3")
            {
                //IJKL Movement
                float horizontal = GamePlayerInformation.PlayerInput.Movement.x;
                float vertical = GamePlayerInformation.PlayerInput.Movement.y;
                Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
                rb.AddForce(direction * speed, ForceMode.Impulse);
            }
            else if (Go.tag == "P4")
            {
                //ArrowKey Movement
                float horizontal = GamePlayerInformation.PlayerInput.Movement.x;
                float vertical = GamePlayerInformation.PlayerInput.Movement.y;
                Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
                rb.AddForce(direction * speed, ForceMode.Impulse);
            }
        }
    }
    #endregion
    #region TriggerDoor
    void OnTriggerExit(Collider other)
    {
        score1 = 0;
        score2 = 0;
        score3 = 0;
        score4 = 0;
        Vector3 pos = new Vector3(1000000, 0, 0);
        if (other.tag == "Door")
        {
            if (other.GetComponentInChildren<TextMeshProUGUI>().text == GameObject.Find("InvText").GetComponent<TextMeshProUGUI>().text)
            {
                GameObject.Find("MinigameManager").GetComponent<ManagerOfTheMinigame>().PlayersWhoPassedTrue.Add(Go);
            }
            else
            {
                GameObject.Find("MinigameManager").GetComponent<ManagerOfTheMinigame>().PlayersWhoPassedFalse.Add(Go);
            }

            Go.GetComponent<Rigidbody>().MovePosition(pos);

            if (GameObject.Find("MinigameManager").GetComponent<ManagerOfTheMinigame>().PlayersWhoPassedFalse.Count + GameObject.Find("MinigameManager").GetComponent<ManagerOfTheMinigame>().PlayersWhoPassedTrue.Count == 4)
            {
                int counter = 4;
                foreach (var Player in GameObject.Find("MinigameManager").GetComponent<ManagerOfTheMinigame>().PlayersWhoPassedTrue)
                {
                    if (Player.tag == "P1")
                    {
                        score1 = score1 + counter;
                    }
                    if (Player.tag == "P2")
                    {
                        score2 = score2 + counter;
                    }
                    if (Player.tag == "P3")
                    {
                        score3 = score3 + counter;
                    }
                    if (Player.tag == "P4")
                    {
                        score4 = score4 + counter;
                    }
                    counter--;
                }
                GameObject.Find("MinigameManager").GetComponent<ManagerOfTheMinigame>().ScoreHandler(Convert.ToInt32(GameObject.Find("MinigameManager").GetComponent<ManagerOfTheMinigame>().ScoreP1.text) + score1, Convert.ToInt32(GameObject.Find("MinigameManager").GetComponent<ManagerOfTheMinigame>().ScoreP2.text) + score2, Convert.ToInt32(GameObject.Find("MinigameManager").GetComponent<ManagerOfTheMinigame>().ScoreP3.text) + score3, Convert.ToInt32(GameObject.Find("MinigameManager").GetComponent<ManagerOfTheMinigame>().ScoreP4.text) + score4);
                GameObject.Find("MinigameManager").GetComponent<ManagerOfTheMinigame>().PlayersWhoPassedFalse.Clear();
                GameObject.Find("MinigameManager").GetComponent<ManagerOfTheMinigame>().PlayersWhoPassedTrue.Clear();
                GameObject.Find("MinigameManager").GetComponent<ManagerOfTheMinigame>().AfterRound();
            }
        }
    }
    #endregion
}
