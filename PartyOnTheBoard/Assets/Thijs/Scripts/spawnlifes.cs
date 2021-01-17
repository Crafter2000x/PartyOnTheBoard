using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnlifes : MonoBehaviour
{
    public Vector3 center;
    public Vector3 size;
    public int i = 0;
    public GameObject life;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waiter());
        i++;
    }
    IEnumerator waiter()
    {
        yield return new WaitForSeconds(10);
        StartCoroutine(waiter());
        spawnlife();
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
    void spawnlife()
    {
        if (MiniGameManagerMeteor.instance.deadplayers.Count < 4)
        {
            Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
            Instantiate(life, pos, Quaternion.identity);
            i++;
        }
        else
        {

        }
        
    }
    
}
