using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiniGameManagerMeteor : MonoBehaviour
{
    public static MiniGameManagerMeteor instance;
    [SerializeField]private TextMeshProUGUI levensplayer1;
    [SerializeField] private TextMeshProUGUI levensplayer2;
    [SerializeField] private TextMeshProUGUI levensplayer3;
    [SerializeField] private TextMeshProUGUI levensplayer4;
    [SerializeField] private TextMeshProUGUI first;
    [SerializeField] private TextMeshProUGUI second;
    [SerializeField] private TextMeshProUGUI third;
    [SerializeField] private TextMeshProUGUI fourth;
    [SerializeField] private TextMeshProUGUI numberone;
    [SerializeField] private TextMeshProUGUI numbertwo;
    [SerializeField] private TextMeshProUGUI numberthree;
    [SerializeField] private TextMeshProUGUI numberfour;
    public List<string> deadplayers = new List<string> ();
    public List<GameObject> playergameobject = new List<GameObject>();
    public int lifes=5;
    public int lifes2=5;
    public int lifes3=5;
    public int lifes4=5;
    public bool gameover = false;
    [SerializeField] private Camera cam1;
    [SerializeField] private Camera cam2;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject player2;
    [SerializeField] private GameObject player3;
    [SerializeField] private GameObject player4;
    [SerializeField] private GameBoardManager GameBoardManager;
    [SerializeField] private List<Transform> Winners;


    int i = 0;

    void Awake()
    {
        MakeInstance();
        levensplayer1 = GameObject.Find("levensplayer1").GetComponent<TextMeshProUGUI>();
        levensplayer2 = GameObject.Find("levensplayer2").GetComponent<TextMeshProUGUI>();
        levensplayer3 = GameObject.Find("levensplayer3").GetComponent<TextMeshProUGUI>();
        levensplayer4 = GameObject.Find("levensplayer4").GetComponent<TextMeshProUGUI>();
        player = GameObject.Find("player");
        player2 = GameObject.Find("Player2");
        player3 = GameObject.Find("Player3");
        player4 = GameObject.Find("Player4");
        cam1.enabled = true;
        cam2.enabled = false;
        first.enabled = false;
        second.enabled = false;
        third.enabled = false;
        fourth.enabled = false;
        numberone.enabled = false;
        numbertwo.enabled = false;
        numberthree.enabled = false;
        numberfour.enabled = false;
    }

    private void OnEnable()
    {
        int counter = 0;
        GameBoardManager = GameObject.Find("GameBoardManager").GetComponent<GameBoardManager>();

        foreach (var Player in GameBoardManager.PlayerList)
        {
            switch (counter)
            {
                case 0: player.GetComponent<Player1>().GamePlayerInfo = Player.GetComponent<GamePlayerInformation>();
                    break;
                case 1:
                    player2.GetComponent<Player2>().GamePlayerInfo = Player.GetComponent<GamePlayerInformation>();
                    break;
                case 2:
                    player3.GetComponent<Player3>().GamePlayerInfo = Player.GetComponent<GamePlayerInformation>();
                    break;
                case 3:
                    player4.GetComponent<Player4>().GamePlayerInfo = Player.GetComponent<GamePlayerInformation>();
                    break;
            }
            counter++;
        }


    }

    void Update()
    {

        if (deadplayers.Count == 4)
        {
            Player1.instance.col1.enabled = true;
            Player2.instance.col2.enabled = true;
            Player3.instance.col3.enabled = true;
            Player4.instance.col4.enabled = true;
            
            levensplayer1.enabled=false;
            levensplayer2.enabled=false;
            levensplayer3.enabled =false;
            levensplayer4.enabled=false;
            first.enabled = true;
            second.enabled = true;
            third.enabled = true;
            fourth.enabled = true;
            numberone.enabled = true;
            numbertwo.enabled = true;
            numberthree.enabled = true;
            numberfour.enabled = true;
            first.text = deadplayers[3];
            second.text =deadplayers[2];
            third.text = deadplayers[1];
            fourth.text = deadplayers[0];
            
            gameover = true;
            cam1.enabled = false;
            cam2.enabled = true;
            while (i == 0)
            {
                Vector3 vector3 = new Vector3(0, 0, 0);
                playergameobject[3].GetComponent<CapsuleCollider>().enabled = true;
                playergameobject[3].transform.parent = Winners[0];
                playergameobject[3].transform.localPosition = vector3;

                playergameobject[2].GetComponent<CapsuleCollider>().enabled = true;
                playergameobject[2].transform.parent = Winners[1];
                playergameobject[2].transform.localPosition = vector3;

                playergameobject[1].GetComponent<CapsuleCollider>().enabled = true;
                playergameobject[1].transform.parent = Winners[2];
                playergameobject[1].transform.localPosition = vector3;

                playergameobject[0].GetComponent<CapsuleCollider>().enabled = true;
                playergameobject[0].transform.parent = Winners[3];
                playergameobject[0].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
                playergameobject[0].transform.localPosition = vector3;

                i++;
            }
        }
    }

    private void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
    public void IncrementLife(int number)
    {
        if (number == 1)
        {
            lifes++;
            if (lifes > 5)
            {
                lifes = 5;
            }
            levensplayer1.text = "Player 1 " +
                "Lifes:"+ lifes;
        }
        if (number == 2)
        {
            lifes2++;
            if (lifes2 > 5)
            {
                lifes2 = 5;
            }
            levensplayer2.text = "Player 2 " +
                "Lifes:"+ lifes2;
        }
        if (number == 3)
        {
            lifes3++;
            if (lifes3 > 5)
            {
                lifes3 = 5;
            }
            levensplayer3.text = "Player 3 " +
                "Lifes:"+ lifes3;
        }
        if (number == 4)
        {
            lifes4++;
            if (lifes4 > 5)
            {
                lifes4 = 5;
            }
            levensplayer4.text = "Player 4 " +
                "Lifes:"+ lifes4;
        }
    }
    public void DecrementLife(int number)
    {
        if (number == 1)
        {
            lifes--;
            levensplayer1.text = "Player 1 " +
                "Lifes:"+ lifes;
            if (lifes == 0)
            {
                deadplayers.Add("Player 1");
                playergameobject.Add(player);
            }
        }
        if (number == 2)
        {
            lifes2--;
            levensplayer2.text = "Player 2 " +
                "Lifes:"+ lifes2;
            if (lifes2 == 0)
            {
                deadplayers.Add("Player 2");
                playergameobject.Add(player2);
            } 
        }
        if (number == 3)
        {
            lifes3--;
            levensplayer3.text = "Player 3 " +
                "Lifes:"+ lifes3;
            if (lifes3 == 0)
            {
                deadplayers.Add("Player 3");
                playergameobject.Add(player3);
            }
        }
        if (number == 4)
        {
            lifes4--;
            levensplayer4.text = "Player 4 " +
                "Lifes:"+ lifes4;
            if (lifes4 == 0)
            {
                deadplayers.Add("Player 4");
                playergameobject.Add(player4);
            }
        }
    }
}
