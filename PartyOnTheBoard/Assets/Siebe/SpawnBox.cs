using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBox : MonoBehaviour
{
    [SerializeField] private Vector3 center;
    [SerializeField] private Vector3 size;
    [SerializeField] private Vector3 sizeBig;
    [SerializeField] private List<GameObject> BoxPrefabs;
    [SerializeField] private Material doosDrop;


    int i = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PressAButtonOnce() 
    {
        if (MinigameManager.instance.countDown < 0)
        {
            while (i == 0)
            {
                StartCoroutine(Delay());
                i++;

            }
        }
    }

    void SpawnBoxes()
    {
        GameObject Prefab = null;
        int RandomNumber = Random.Range(0, 550);
        if (RandomNumber <= 50)
        {
            Prefab = BoxPrefabs[0];
            Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
            Instantiate(Prefab, pos, Quaternion.identity);
        }
        else if (RandomNumber >= 101 && RandomNumber < 150)
        {
            Prefab = BoxPrefabs[1];
            Vector3 pos = new Vector3(3.48f, 3, 70);
            Instantiate(Prefab, pos, Quaternion.identity);
        }
        else if (RandomNumber >= 51 && RandomNumber < 100)
        {
            Prefab = BoxPrefabs[2];
            Vector3 pos = center + new Vector3(Random.Range(-sizeBig.x / 2, size.x / 2), Random.Range(-sizeBig.y / 2, sizeBig.y / 2), Random.Range(-sizeBig.z / 2, sizeBig.z / 2));
            Instantiate(Prefab, pos, Quaternion.identity);
        }
        else if (RandomNumber >= 151 && RandomNumber < 200)
        {
            Prefab = BoxPrefabs[3];
            Vector3 pos = center + new Vector3(Random.Range(-sizeBig.x / 2, size.x / 2), Random.Range(-sizeBig.y / 2, sizeBig.y / 2), Random.Range(-sizeBig.z / 2, sizeBig.z / 2));
            Instantiate(Prefab, pos, Quaternion.identity);
        }
        else if (RandomNumber >= 201 && RandomNumber < 250)
        {
            Prefab = BoxPrefabs[4];
            Vector3 pos = center + new Vector3(Random.Range(-sizeBig.x / 2, size.x / 2), Random.Range(-sizeBig.y / 2, sizeBig.y / 2), Random.Range(-sizeBig.z / 2, sizeBig.z / 2));
            Instantiate(Prefab, pos, Quaternion.identity);
        }
        else if (RandomNumber >= 251 && RandomNumber < 300)
        {
            Prefab = BoxPrefabs[5];
            Vector3 pos = center + new Vector3(Random.Range(-sizeBig.x / 2, size.x / 2), Random.Range(-sizeBig.y / 2, sizeBig.y / 2), Random.Range(-sizeBig.z / 2, sizeBig.z / 2));
            Instantiate(Prefab, pos, Quaternion.identity);
        }
        else if (RandomNumber >= 301 && RandomNumber < 350)
        {
            Prefab = BoxPrefabs[6];
            Vector3 pos = center + new Vector3(Random.Range(-sizeBig.x / 2, size.x / 2), Random.Range(-sizeBig.y / 2, sizeBig.y / 2), Random.Range(-sizeBig.z / 2, sizeBig.z / 2));
            Instantiate(Prefab, pos, Quaternion.identity);
        }
        else if (RandomNumber >= 351 && RandomNumber < 400)
        {
            Prefab = BoxPrefabs[7];
            Vector3 pos = new Vector3(3.48f, 3, 70);
            Instantiate(Prefab, pos, Quaternion.identity);
        }
        else if (RandomNumber >= 451 && RandomNumber < 500)
        {
            Prefab = BoxPrefabs[8];
            Vector3 pos = center + new Vector3(Random.Range(-sizeBig.x / 2, size.x / 2), Random.Range(-sizeBig.y / 2, sizeBig.y / 2), Random.Range(-sizeBig.z / 2, sizeBig.z / 2));
            Instantiate(Prefab, pos, Quaternion.identity);
        }
        else if (RandomNumber >= 501 && RandomNumber < 550)
        {
            Prefab = BoxPrefabs[9];
            Vector3 pos = center + new Vector3(Random.Range(-sizeBig.x / 2, size.x / 2), Random.Range(-sizeBig.y / 2, sizeBig.y / 2), Random.Range(-sizeBig.z / 2, sizeBig.z / 2));
            Instantiate(Prefab, pos, Quaternion.identity);
        }
    }

    IEnumerator Delay()
    {
        i++;
        SpawnBoxes();
        doosDrop.color = Color.red;
        yield return new WaitForSeconds(2);
        doosDrop.color = Color.green;
        i = 0;
    }
}
