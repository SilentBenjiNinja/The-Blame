using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour {

    public bool actionDoneThisRound;
    public float thisWorkloadValue;
    private float blameValue;
    public float WORKLOADSHIFTVALUE = 10;

    private void Start()
    {
        actionDoneThisRound = false;
    }

    public void newRound()
    {
        actionDoneThisRound = false;
    }
    
    public void pushWorkload(PlayerActions receiver)
    {
        if (!actionDoneThisRound)
        {
            actionDoneThisRound = true;
            receiver.plusWorkload(WORKLOADSHIFTVALUE);
            thisWorkloadValue -= WORKLOADSHIFTVALUE;
        }
        else
        {
            noInputPossible();
        }
    }

    public void pullWorkload(PlayerActions giver)
    {
        if (!actionDoneThisRound)
        {
            actionDoneThisRound = true;
            giver.minusWorkload(WORKLOADSHIFTVALUE);
            thisWorkloadValue += WORKLOADSHIFTVALUE;
        }
        else
        {
            noInputPossible();
        }
    }

    public void plusWorkload(float value)
    {
        thisWorkloadValue += value;
    }

    public void minusWorkload(float value)
    {
        thisWorkloadValue -= value;
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
