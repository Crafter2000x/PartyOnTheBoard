using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomspawn : MonoBehaviour
{
    public Vector3 center;
    public Vector3 size;
    public GameObject MeteorPrefab;
    public int i = 0;
    public AudioSource start;
    private bool hasrun = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waiter());
        i++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator waiter()
    {
        if (hasrun==false)
        {
            yield return new WaitForSeconds(4);
            hasrun = true;
            spawnmeteors();
        }
        if (i > 0 && i < 10)
        {
            yield return new WaitForSeconds(2);
            spawnmeteors();
        }
        else if (i >= 10 && i < 20)
        {
            yield return new WaitForSeconds(1);
            spawnmeteors();
        }
        else if (i > 19&&i<50)
        {
            yield return new WaitForSeconds(0.8f);
            spawnmeteors();
        }
        else if (i >= 50)
        {
            yield return new WaitForSeconds(0.6f);
            spawnmeteors();
        }
        StartCoroutine(waiter());

    }
        void spawnmeteors()
        {
            if (MiniGameManagerMeteor.instance.deadplayers.Count < 4)
            {
            Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
            Instantiate(MeteorPrefab, pos, Quaternion.identity);
            i++;
            }
            else
            {

            }
            
        }
    
    
}
