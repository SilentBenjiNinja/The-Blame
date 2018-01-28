using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerActions : MonoBehaviour {

    public enum Department { HR, Sales, Management, IT }

    public bool actionDoneThisRound;
    public float thisWorkloadValue;
    public float maxWorkloadValue = 100;
    public float WORKLOADSHIFTVALUE = 10;
    public Department myDepartment;

    public float minBlameWorkout = 25;
    public float maxBlameWorkout = 75;
    public float blameSpeed = 0.1f;

    public Text buttonText;
    public Slider sliderWorkload;

    private float blameValue;

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

    private void FixedUpdate()
    {
        if (thisWorkloadValue > maxBlameWorkout || thisWorkloadValue < minBlameWorkout)
        {
            blameValue += blameSpeed;
        }
        else
        {
            blameValue -= blameSpeed;
        }
        Debug.Log(blameValue);
    }

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

			if (sliderWorkload == null) {

				Scene benjiScene = SceneManager.GetSceneByName ("Benji_Scene");

				GameObject[] roots = benjiScene.GetRootGameObjects ();
				for (int i = 0; i < roots.Length && sliderWorkload==null; i++) {
					Transform sliderTF = roots [i].transform.Find ("BlameMeter");
					if (sliderTF != null && sliderTF.gameObject != null) {
						Slider slider = sliderTF.GetComponent<Slider> ();
						if (slider != null) {
							sliderWorkload = slider;
						}
					}
				}
			}

			if (sliderWorkload != null) {
				sliderWorkload.maxValue = maxWorkloadValue;

				UICheat cheat = sliderWorkload.GetComponent<UICheat> ();
				buttonText = cheat.buttonTexts [(int)myDepartment];
			}

		}
	}

    private void LateUpdate()
    {
		if (sliderWorkload != null) {
			sliderWorkload.value = thisWorkloadValue / maxWorkloadValue;
		}

		if (buttonText != null) {
			buttonText.text = myDepartment.ToString ();
		}
    }
}
