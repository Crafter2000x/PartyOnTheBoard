using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidePlayer : MonoBehaviour
{
    [SerializeField] private GameObject Go;
    [SerializeField] private Rigidbody rb1;
    [SerializeField] private Rigidbody rb2;
    [SerializeField] private Rigidbody rb3;
    [SerializeField] private float Bounce;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player1")
        {
            Debug.Log("Raakt");
            rb1.AddForce(Vector3.back * Bounce, ForceMode.Impulse);
            rb1.velocity = Vector3.zero;
        }
        else if (col.gameObject.tag == "Player2")
        {
            Debug.Log("Raakt");
            rb2.AddForce(Vector3.back * Bounce, ForceMode.Impulse);
            rb2.velocity = Vector3.zero;
        }
        else if (col.gameObject.tag == "Player3")
        {
            Debug.Log("Raakt");
            rb3.AddForce(Vector3.back * Bounce, ForceMode.Impulse);
            rb3.velocity = Vector3.zero;
        }
    }

    

    
}
