using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour {

    public bool actionDoneThisRound;
    public GameObject manager;


    private void Start()
    {
        actionDoneThisRound = false;
    }

    public void newRound()
    {
        actionDoneThisRound = false;
    }
    
    public void shiftWorkToPlayer(int playerId)
    {

        actionDoneThisRound = true;
    }
}
