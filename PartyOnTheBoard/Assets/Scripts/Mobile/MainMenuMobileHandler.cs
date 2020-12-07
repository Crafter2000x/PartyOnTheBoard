using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMobileHandler : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Canvas JoinLobbyCanvas = null;


    public void DisplayLobbyCanvas()
    {
        JoinLobbyCanvas.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
