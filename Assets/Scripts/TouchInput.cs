using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchInput : MonoBehaviour {

    public string roomName = "Blame";

    public Text testText1;
    public Text testText2;

    private int localValue = 10;
    public int globalValue = 10;
    
    public void changeGlobal()
    {
        globalValue += 1;
        testText1.text = "ValueGlobal: " + globalValue;
    }

    public void changeLocal()
    {
        localValue += 1;
        testText2.text = "ValueLocal: " + localValue;
    }


}
