using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoosCollide : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Doos")
        {
            Destroy(col.gameObject);
        }
    }
}
