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

    private void Start()
    {
        hostGame = GameObject.Find("HostGame").GetComponent<Button>();
        joinGame = GameObject.Find("JoinGame").GetComponent<Button>();
        howToPlay = GameObject.Find("HowToPlay").GetComponent<Button>();
        backButton = GameObject.Find("BackButton").GetComponent<Button>();
        endGame = GameObject.Find("ExitGame").GetComponent<Button>();
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
        hostGame.enabled = false;
        joinGame.enabled = false;
        howToPlay.enabled = false;
    }

    private void DectivateStartmenu()
    {
        hostGame.enabled = false;
        joinGame.enabled = false;
        howToPlay.enabled = false;
    }

    public void OpenHowToPlay()
    {
        DectivateStartmenu();
    }
}
