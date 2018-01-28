using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour {

    private GameObject startMenu;
    private GameObject howToPlay;
    private GameObject nameInput;
    private GameObject lobby;
    private GameObject waitingForPlayer;

    //private Image startMenu;
    private Image gameTitle;
    private Image howToPlayBackground;

    private void Start()
    {
        startMenu = GameObject.Find("StartMenu");
        if(startMenu != null)
        {

        waitingForPlayer = GameObject.Find("WaitingForPlayer");
        lobby = GameObject.Find("Lobby");
        howToPlay = GameObject.Find("HowToPlayBackground");
        nameInput = GameObject.Find("NameInput");
        howToPlay.SetActive(false);
        nameInput.SetActive(false);
        lobby.SetActive(false);
        waitingForPlayer.SetActive(false);
        }

    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GoBack()
    {

        if (howToPlay.activeSelf)
        {
            howToPlay.SetActive(false);
            ActivateStartmenu();
        }else if (lobby.activeSelf)
        {
            lobby.SetActive(false);
            ActivateStartmenu();
        }
        else if (nameInput.activeSelf)
        {
            nameInput.SetActive(false);
            ActivateStartmenu();
        }
        else if (waitingForPlayer.activeSelf)
        {
            waitingForPlayer.SetActive(false);
            ActivateStartmenu();
        }


    }

    private void ActivateStartmenu()
    {

        startMenu.SetActive(true);
    }

    private void DectivateStartmenu()
    {
        startMenu.SetActive(false);
    }

    public void OpenHowToPlay()
    {
        DectivateStartmenu();
        howToPlay.SetActive(true);
    }
}
