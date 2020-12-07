using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuHandler : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Canvas LobbyCanvas = null;


    public void DisplayLobbyCanvas()
    {
        LobbyCanvas.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
