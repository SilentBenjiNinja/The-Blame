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
    private Text gameRules;
    private Text gameRulesHeader;
    private GameObject NextPageButton;

    //private Image startMenu;
    private Image gameTitle;
    private Image howToPlayBackground;

    private void Start()
    {
        startMenu = GameObject.Find("StartMenu");
        if(startMenu != null)
        {
            NextPageButton = GameObject.Find("NextPageButton");
            waitingForPlayer = GameObject.Find("WaitingForPlayer");
            lobby = GameObject.Find("Lobby");
            howToPlay = GameObject.Find("HowToPlayBackground");
            nameInput = GameObject.Find("NameInput");
            gameRulesHeader = GameObject.Find("GameRulesHeader").GetComponent<Text>();
            gameRules = GameObject.Find("GameRulesText").GetComponent<Text>();
            gameRules.text = StringCollection.GAMERULESPAGEONE;
            gameRulesHeader.text = StringCollection.GAMERULESONE;
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

        if (howToPlay.activeSelf && gameRules.text == StringCollection.GAMERULESPAGEONE)
        {
            howToPlay.SetActive(false);
            ActivateStartmenu();
        }else if(howToPlay.activeSelf && gameRules.text == StringCollection.GAMERULESPAGETWO)
        {
            NextPageButton.SetActive(true);
            gameRulesHeader.text = StringCollection.GAMERULESONE;
            gameRules.text = StringCollection.GAMERULESPAGEONE;
        }
        else if (howToPlay.activeSelf && gameRules.text == StringCollection.GAMERULESPAGETHREE)
        {
            NextPageButton.SetActive(true);
            gameRulesHeader.text = StringCollection.GAMERULESONE;
            gameRules.text = StringCollection.GAMERULESPAGEONE;
        }
        else if (lobby.activeSelf)
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
        NextPageButton.SetActive(true);
        howToPlay.SetActive(true);
    }

    public void OpenNext()
    {
        if(gameRules.text == StringCollection.GAMERULESPAGEONE)
        {
            gameRulesHeader.text = StringCollection.GAMERULESTWO;
            gameRules.text = StringCollection.GAMERULESPAGETWO;
        }else if(gameRules.text == StringCollection.GAMERULESPAGETWO)
        {
            gameRulesHeader.text = StringCollection.GAMERULESTHREE;
            gameRules.text = StringCollection.GAMERULESPAGETHREE;
            NextPageButton.SetActive(false);
        }
    }
}
