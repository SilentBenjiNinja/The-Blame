using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour {

    private Button hostGame;
    private Button joinGame;
    private Button howToPlay;
    private Button backButton;
    private Button endGame;

    //private Image startMenu;
    private Image gameTitle;
    private Image howToPlayBackground;

    private void Start()
    {
        howToPlayBackground = GameObject.Find("HowToPlayBackground").GetComponent<Image>();
        //startMenu = GameObject.Find("StartMenu").GetComponent<Image>();
        gameTitle = GameObject.Find("GameTitle").GetComponent<Image>();
        hostGame = GameObject.Find("HostGame").GetComponent<Button>();
        joinGame = GameObject.Find("JoinGame").GetComponent<Button>();
        howToPlay = GameObject.Find("HowToPlay").GetComponent<Button>();
        backButton = GameObject.Find("Back").GetComponent<Button>();
        //endGame = GameObject.Find("ExitGame").GetComponent<Button>();
        backButton.enabled = false;
        howToPlayBackground.enabled = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GoBack()
    {
        //if(hostGame.enabled == false)
        //{
        //    ActivateStartmenu();
        //}

        

    }

    private void ActivateStartmenu()
    {

        howToPlay.enabled = false;
    }

    private void DectivateStartmenu()
    {
        hostGame.enabled = false;
        joinGame.enabled = false;
        
    }

    public void OpenHowToPlay()
    {
        gameTitle.enabled = false;
        howToPlayBackground.enabled = true;
    }
}
