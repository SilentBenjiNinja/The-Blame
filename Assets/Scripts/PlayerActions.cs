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
	public float maxBlameValue = 100;
    public float WORKLOADSHIFTVALUE = 10;
    public Department myDepartment;

    public float minBlameWorkout = 25;
    public float maxBlameWorkout = 75;
    public float blameSpeed = 0.1f;

    public Text buttonText;
    public Slider sliderWorkload;
	public Slider sliderBlame;

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
		if (sliderBlame) {
			sliderBlame.value = blameValue / maxBlameValue;
		}
    }

    void Update(){
		if (!init && roundBasedGame.instance!=null && roundBasedGame.instance.playersArray!=null) {

			// assign a free department to the player
			for (int i = 0; i < depNames.Length; i++) {
				if (!roundBasedGame.instance.playerMap.ContainsKey ((Department)i) || roundBasedGame.instance.playerMap [(Department)i] == null) {
					myDepartment = (Department)i;
					roundBasedGame.instance.playerMap.Add (myDepartment, this);
				}
			}

			if (sliderBlame == null) {

				Scene benjiScene = SceneManager.GetSceneByName ("Benji_Scene");

				GameObject[] roots = benjiScene.GetRootGameObjects ();
				for (int i = 0; i < roots.Length && sliderBlame==null; i++) {
					Transform sliderTF = roots [i].transform.Find ("BlameMeter");
					if (sliderTF != null && sliderTF.gameObject != null) {
						Slider slider = sliderTF.GetComponent<Slider> ();
						if (slider != null) {
							sliderBlame = slider;
						}
					}
				}
			}

			if (sliderWorkload != null) {
				sliderWorkload.maxValue = maxWorkloadValue;

				UICheat cheat = sliderWorkload.GetComponent<UICheat> ();
				if (cheat != null) {
					buttonText = cheat.buttonTexts [(int)myDepartment];
					sliderWorkload = cheat.workloadSliders[(int)myDepartment];
				}
			}

			init = true;

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
