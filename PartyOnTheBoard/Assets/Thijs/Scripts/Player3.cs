using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player3 : MonoBehaviour
{
    public GamePlayerInformation GamePlayerInfo;
    public static Player3 instance;
    [SerializeField] private Rigidbody rb;
    bool isgrounded = true;
    public float speed = 8f;
    public int lifes = 5;
    public AudioSource audiojump;
    [Header("UI")]
    private MiniGameManagerMeteor MiniManage;
    [SerializeField] public Collider col3;

    // Start is called before the first frame update
    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        MiniManage=GetComponent<MiniGameManagerMeteor>();
        MakeInstance();
    }
    private void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void OnEnable()
    {
        if (GamePlayerInfo != null)
        {
            GamePlayerInfo.PlayerInput.OnVariableChangeInputA += JumpButton;
        }
    }

    private void JumpButton(bool newVal)
    {
        if (MiniGameManagerMeteor.instance.gameover == false)
        {
            if (MiniGameManagerMeteor.instance.deadplayers.Count < 4)
            {
                if (isgrounded == true)
                {
                    jump();
                    rb.AddForce(Vector3.up * 80, ForceMode.Impulse);
                }
            }
        }

        if (MiniGameManagerMeteor.instance.gameover == true)
        {
            if (isgrounded == true)
            {
                rb.AddForce(Vector3.up * 80, ForceMode.Impulse);
            }
            else if (isgrounded == false)
            {

            }
        }
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        if (MiniGameManagerMeteor.instance.gameover == false)
        {
            if (MiniGameManagerMeteor.instance.deadplayers.Count < 4 && GamePlayerInfo != null)
            {
                if (isgrounded == true)
                {
                    if (GamePlayerInfo.PlayerInput.Movement.x < 0)
                    {
                        rb.AddForce(Vector3.left * speed, ForceMode.Impulse);
                    }
                    if (GamePlayerInfo.PlayerInput.Movement.x > 0)
                    {
                        rb.AddForce(Vector3.right * speed, ForceMode.Impulse);
                    }
                }
                else if (isgrounded == false && GamePlayerInfo != null)
                {

                    if (GamePlayerInfo.PlayerInput.Movement.x < 0)
                    {
                        rb.AddForce(Vector3.left * 1f, ForceMode.Impulse);
                    }
                    if (GamePlayerInfo.PlayerInput.Movement.x > 0)
                    {
                        rb.AddForce(Vector3.right * 1f, ForceMode.Impulse);
                    }
                }
            }
        }
    }

    void jump()
    {
        audiojump.Play();
    }


    void OnCollisionEnter(Collision theCollision)
    {
        if (theCollision.gameObject.tag == "floor")
        {
            isgrounded = true;
        }
        if (theCollision.gameObject.tag == "life")
        {
            MiniGameManagerMeteor.instance.IncrementLife(3);
        }
        if (theCollision.gameObject.tag == "meteoor")
        {
            MiniGameManagerMeteor.instance.DecrementLife(3);
            if (MiniGameManagerMeteor.instance.lifes3 == 0)
            {
                col3.enabled = !col3.enabled;
            }
        }
    }


    //consider when character is jumping .. it will exit collision.
    void OnCollisionExit(Collision theCollision)
    {
        if (theCollision.gameObject.tag == "floor")
        {
            isgrounded = false;
        }
    }
}
