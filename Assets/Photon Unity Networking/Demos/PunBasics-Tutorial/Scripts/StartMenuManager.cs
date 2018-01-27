using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour {

    private GameObject startMenu;
    private GameObject howToPlay;
    private GameObject nameInput;

    //private Image startMenu;
    private Image gameTitle;
    private Image howToPlayBackground;

    private void Start()
    {
        startMenu = GameObject.Find("StartMenu");
        howToPlay = GameObject.Find("HowToPlayBackground");
        nameInput = GameObject.Find("NameInput");
        howToPlay.SetActive(false);
        nameInput.SetActive(false);
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
