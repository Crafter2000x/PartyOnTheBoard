using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class demoscript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI demo;
    int i = 0;


    void Update()
    {
        while (i == 0)
        {
            StartCoroutine(Delay());
            i++;



        }
        IEnumerator Delay()
        {
            i++;
            demo.color = Color.yellow;
            yield return new WaitForSeconds(0.5f);
            demo.color = Color.black;
            yield return new WaitForSeconds(0.5f);
            i = 0;
        }

    }
    

}
