using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroylife : MonoBehaviour
{
    public float rotationSpeed = 99.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            transform.Rotate(Vector3.forward * Time.deltaTime * this.rotationSpeed);
    }

    public void SetRotationSpeed(float speed)
    {
        this.rotationSpeed = speed;
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "otherplayer")
        {
            Destroy(gameObject);
        }else if (other.gameObject.tag == "meteoor")
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "floor")
        {
            Destroy(gameObject, 2f);
        }
        else
        {
            Destroy(gameObject, 3f);
        }
        
    }
}
