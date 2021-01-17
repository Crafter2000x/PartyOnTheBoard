using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision Collision)
    {
        
        if (Collision.gameObject.name == "floor")
        {   
            Destroy(gameObject,0.08f);
        }
        if (Collision.gameObject.tag == "otherplayer")
        { 
            Destroy(gameObject);
        }
        if (Collision.gameObject.tag == "meteoor")
        { 
            Destroy(gameObject);
        }
        if (Collision.gameObject.tag == "life")
        {
            Destroy(gameObject);
        }
    }
}
