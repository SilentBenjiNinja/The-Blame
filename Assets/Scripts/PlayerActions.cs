using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour {

    public enum Department { HR, Sales, Management, IT }

    public bool actionDoneThisRound;
    public float thisWorkloadValue;
    private float blameValue;
    public float WORKLOADSHIFTVALUE = 10;
    public Department myDepartment;

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
    private string[] depNames = System.Enum.GetNames(typeof(Department));
    

    void Update(){
		if (!init && roundBasedGame.instance!=null && roundBasedGame.instance.playersArray!=null) {
			if(!roundBasedGame.instance.playersArray.Contains(this)){
				roundBasedGame.instance.playersArray.Add(this);	
			}
			init = true;
            List<string> alreadyAssigned = new List<string>();
            foreach (PlayerActions pa in roundBasedGame.instance.playersArray)
            {
                alreadyAssigned.Add(pa.myDepartment.ToString());
            }
            foreach (string depName in depNames)
            {
                if (!alreadyAssigned.Contains(depName))
                {
                    myDepartment = (Department)System.Enum.Parse(typeof(Department), depName);
                    break;
                }
            }
		}
	}
}
