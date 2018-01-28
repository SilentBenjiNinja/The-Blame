using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon;

[RequireComponent(typeof(PhotonView))]
public class PlayerActions : Photon.MonoBehaviour {

    public enum Department { HR, Sales, Management, IT }

    public bool actionDoneThisRound;

    private float thisWorkloadValue;
    public float maxWorkloadValue = 100;
	
    public float WORKLOADSHIFTVALUE = 10;
    public Department myDepartment;

    public float maxBlameValue = 100;
    public float minBlameWorkout = 25;
    public float maxBlameWorkout = 75;
    public float blameSpeed = 0.1f;
    
    private float blameValue;

    public bool hasBlameSticker = false;

    public Text buttonText;
    public Slider sliderWorkload;
	public Slider sliderBlame;

	private bool isPlayer = false;
    
    private void Start()
    {
        actionDoneThisRound = false;

		PhotonView photonView = GetComponent<PhotonView> ();
		if (photonView != null) {
			isPlayer = photonView.owner.IsLocal; 
			name = "I am " + photonView.owner.NickName;
		}
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

    public void getBlameSticker()
    {
        hasBlameSticker = true;
        foreach (PlayerActions player in roundBasedGame.instance.playersArray)
        {
            if (player != this)
            {
                hasBlameSticker = false;
            }
        }
    }

	private bool init = false;
    private string[] depNames = System.Enum.GetNames(typeof(Department));

    private void FixedUpdate()
    {
		if (roundBasedGame.instance==null || !roundBasedGame.instance.HasGameStarted) {
			return;
		}

        if (thisWorkloadValue > maxBlameWorkout || thisWorkloadValue < minBlameWorkout)
        {
            blameValue += blameSpeed;
        }
        else
        {
            blameValue -= blameSpeed;
        }
        if (blameValue >= maxBlameValue)
        {
            blameValue = 0;
            getBlameSticker();
        }
		if (sliderBlame) {
			sliderBlame.value = blameValue / maxBlameValue;
		}
    }

    void Update(){
		if (!init && roundBasedGame.instance!=null && roundBasedGame.instance.playersArray!=null) {

			// assign a free department to the player
			for (int i = 0; i < depNames.Length; i++) {
				if (!roundBasedGame.instance.playerMap.ContainsValue (this)) {
					if (!roundBasedGame.instance.playerMap.ContainsKey ((Department)i) || roundBasedGame.instance.playerMap [(Department)i] == null) {
						myDepartment = (Department)i;
						if (roundBasedGame.instance.playerMap.ContainsKey ((Department)i) && roundBasedGame.instance.playerMap [(Department)i] == null) {
							roundBasedGame.instance.playerMap[(Department)i] = this;
						} else {
							roundBasedGame.instance.playerMap.Add (myDepartment, this);
						}
					}
				}
			}
			if (!roundBasedGame.instance.playersArray.Contains (this)) {
				roundBasedGame.instance.playersArray.Add (this);
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

			if (sliderBlame != null) {
				
				UICheat cheat = sliderBlame.GetComponent<UICheat> ();
				if (cheat != null) {
					buttonText = cheat.buttonTexts [(int)myDepartment];
					sliderWorkload = cheat.workloadSliders[(int)myDepartment];

					if (isPlayer) {
						cheat.gotoPlayer.transformButton = buttonText.transform;
						cheat.gotoPlayer.enabled = true;
					}
				}

				if (sliderWorkload != null) {
					//sliderWorkload.maxValue = maxWorkloadValue;
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


	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(thisWorkloadValue);
			stream.SendNext((int)myDepartment);
			stream.SendNext(blameValue);
			stream.SendNext(hasBlameSticker);
			Debug.Log(name + " is Sending: " + info.sender.NickName);
		}
		else
		{
			thisWorkloadValue = (float)stream.ReceiveNext();

			Department oldDept = myDepartment;
			myDepartment = (Department)((int)stream.ReceiveNext());
			if (oldDept != myDepartment) {
				roundBasedGame.instance.playerMap [oldDept] = null;
				if (roundBasedGame.instance.playerMap.ContainsKey (myDepartment)) {
					roundBasedGame.instance.playerMap [myDepartment] = this;
				} else {
					roundBasedGame.instance.playerMap.Add (myDepartment, this);
				}
			}

			blameValue = (float)stream.ReceiveNext();
			hasBlameSticker = (bool)stream.ReceiveNext();
			Debug.Log(name + " has Received from: " + info.sender.NickName);
		}
	}
}
