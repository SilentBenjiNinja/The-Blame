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
    private AudioSource clickSound;

    //private Image startMenu;
    private Image gameTitle;
    private Image howToPlayBackground;

    private void Start()
    {
        startMenu = GameObject.Find("StartMenu");
        if(startMenu != null)
        {
            clickSound = GameObject.Find("ClickSoundManager").GetComponent<AudioSource>();
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
            clickSound.Play();
            howToPlay.SetActive(false);
            ActivateStartmenu();
        }else if(howToPlay.activeSelf && gameRules.text == StringCollection.GAMERULESPAGETWO)
        {
            clickSound.Play();
            NextPageButton.SetActive(true);
            gameRulesHeader.text = StringCollection.GAMERULESONE;
            gameRules.text = StringCollection.GAMERULESPAGEONE;
        }
        else if (howToPlay.activeSelf && gameRules.text == StringCollection.GAMERULESPAGETHREE)
        {
            clickSound.Play();
            NextPageButton.SetActive(true);
            gameRulesHeader.text = StringCollection.GAMERULESONE;
            gameRules.text = StringCollection.GAMERULESPAGEONE;
        }
        else if (lobby.activeSelf)
        {
            clickSound.Play();
            lobby.SetActive(false);
            ActivateStartmenu();
        }
        else if (nameInput.activeSelf)
        {
            clickSound.Play();
            nameInput.SetActive(false);
            ActivateStartmenu();
        }
        else if (waitingForPlayer.activeSelf)
        {
            clickSound.Play();
            waitingForPlayer.SetActive(false);
            ActivateStartmenu();
        }


    }

    private void ActivateStartmenu()
    {
        clickSound.Play();
        startMenu.SetActive(true);
    }

    private void DectivateStartmenu()
    {
        clickSound.Play();
        startMenu.SetActive(false);
    }

    public void OpenHowToPlay()
    {
        clickSound.Play();
        DectivateStartmenu();
        NextPageButton.SetActive(true);
        howToPlay.SetActive(true);
    }

    public void OpenNext()
    {
        if(gameRules.text == StringCollection.GAMERULESPAGEONE)
        {
            clickSound.Play();
            gameRulesHeader.text = StringCollection.GAMERULESTWO;
            gameRules.text = StringCollection.GAMERULESPAGETWO;
        }else if(gameRules.text == StringCollection.GAMERULESPAGETWO)
        {
            clickSound.Play();
            gameRulesHeader.text = StringCollection.GAMERULESTHREE;
            gameRules.text = StringCollection.GAMERULESPAGETHREE;
            NextPageButton.SetActive(false);
        }
    }
}
