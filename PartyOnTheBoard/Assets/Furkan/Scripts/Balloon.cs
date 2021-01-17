using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public static Balloon Instance;

    public bool IsBlue;
    public bool IsGreen;
    public bool IsBlack;
    public bool IsWhite;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        Destroy(gameObject, 10);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2" || collision.gameObject.tag == "Player3" || collision.gameObject.tag == "Player4")
        {
            if (IsBlue)
            {
                collision.gameObject.GetComponent<PlayerMovement>().score += 100;
                Destroy(this.gameObject);
            }
            if (IsGreen)
            {
                collision.gameObject.GetComponent<PlayerMovement>().score += 300;
                Destroy(this.gameObject);
            }
            if (IsBlack) //Speedboost
            {
                collision.gameObject.GetComponent<PlayerMovement>().Speedboost(true);
                Destroy(this.gameObject);
            }
            if (IsWhite) //Lose Score
            {
                collision.gameObject.GetComponent<PlayerMovement>().score -= 200;
                collision.gameObject.GetComponent<PlayerMovement>().Speedboost(false);
                Destroy(this.gameObject);
            }
        }
    }
}
