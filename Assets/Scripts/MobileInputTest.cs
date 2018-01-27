using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileInputTest : MonoBehaviour {

    public string roomName = "Blame";

    private Button testButton;
    private Text boolText;
    private bool isChanging = false;

	// Use this for initialization
	void Start () {
        
        testButton = GameObject.Find(StringCollection.TESTBUTTON).GetComponent<Button>();
        boolText = GameObject.Find(StringCollection.BOOLTEXT).GetComponent<Text>();
        isChanging = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void SwitchBool()
    {
        isChanging = !isChanging;
        boolText.text = "Your bool is: " + isChanging;
    }

    void OnJoinedRoom()
    {
        
    }

}
