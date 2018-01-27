using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour {


    public bool actionDoneThisRound;

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
        if (!actionDoneThisRound)
        {
            actionDoneThisRound = true;
        }
        else
        {
            noInputPossible();
        }
    }

    private void noInputPossible()
    {
        Debug.Log("Round not over yet.");
    }

	private bool init = false;

	void Update(){
		if (!init && roundBasedGame.instance!=null && roundBasedGame.instance.playersArray!=null) {
			if(!roundBasedGame.instance.playersArray.Contains(this)){
				roundBasedGame.instance.playersArray.Add(this);	
			}
			init = true;
		}
	}
}
