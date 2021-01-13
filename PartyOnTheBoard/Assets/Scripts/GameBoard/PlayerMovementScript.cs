using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public GameObject PlayerNetworkClone;
    [SerializeField] PlayerParty PlayerInput;
    public float MovementSpeed = 2;

    private void Start()
    {
        PlayerInput = PlayerNetworkClone.GetComponent<PlayerParty>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement() 
    {
        if (PlayerNetworkClone != null)
        {
            Vector3 PlayerMovement = new Vector3(PlayerInput.Movement.x, 0f, PlayerInput.Movement.y) * MovementSpeed * Time.deltaTime;
            transform.Translate(PlayerMovement);
        }
    }
}
