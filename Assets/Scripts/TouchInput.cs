using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchInput : MonoBehaviour {

    public string roomName = "Blame";

    public Button testButton;
    public Text boolText;
    private bool isChanging = false;
    
    public void SwitchBool()
    {
        isChanging = !isChanging;
        boolText.text = "Your bool is: " + isChanging;
    }
}
