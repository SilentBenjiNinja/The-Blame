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

		if (workloadToGive == null || workloadToGive.Count<4 || workloadToTake.Count<4) {
			workloadToGive = new List<float> ();
			for (int i = 0; i < 4; i++) {
				workloadToGive.Add (0);
			}
			workloadToTake = new List<float> ();
			for (int i = 0; i < 4; i++) {
				workloadToTake.Add (0);
			}
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
            //receiver.plusWorkload(WORKLOADSHIFTVALUE);
            //thisWorkloadValue -= WORKLOADSHIFTVALUE;

			workloadToGive [(int)receiver.myDepartment] += WORKLOADSHIFTVALUE;
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
            //giver.minusWorkload(WORKLOADSHIFTVALUE);
            //thisWorkloadValue += WORKLOADSHIFTVALUE;

			workloadToTake [(int)giver.myDepartment] += WORKLOADSHIFTVALUE;
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

		if (roundBasedGame.instance == null) {
			return;
		}

		if (roundBasedGame.instance.playersArrayEver != null && !roundBasedGame.instance.playersArrayEver.Contains(this)) {
			roundBasedGame.instance.playersArrayEver.Add (this);
		}

		if (roundBasedGame.instance.playerMap == null) {
			roundBasedGame.instance.playerMap = new Dictionary<Department, PlayerActions> ();
		}
			
			PhotonView pv = GetComponent<PhotonView> ();
			if (pv != null) {
				int dptInt = pv.viewID / 1000;
				myDepartment = (Department)(dptInt - 1);
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

						for (int i = 0; i < cheat.fromButtonList.Count; i++) {
							if (i == (int)myDepartment) {
								cheat.fromButtonList [i].interactable = false;
							} else {
								cheat.fromButtonList [i].interactable = true;
								int ik = i;
								Button otherButton = cheat.fromButtonList [i];
								otherButton.onClick.RemoveAllListeners ();
								otherButton.onClick.AddListener (delegate() {
									pushWorkload (ik);	
									cheat.pickOtherUI.SetActive (false);
								}); 

							/*
								if (roundBasedGame.instance.playerMap.ContainsKey ((Department)i)) {
									PlayerActions receiver = roundBasedGame.instance.playerMap [(Department)i];
									otherButton.onClick.AddListener (delegate() {
										pushWorkload (receiver);	
										cheat.pickOtherUI.SetActive (false);
									}); 
								} else {
									otherButton.interactable = false;
								}
								*/
							}

							Button myButton = cheat.buttonList [(int)myDepartment];
							myButton.onClick.AddListener (delegate() {
							cheat.pickOtherUI.SetActive(true);
							}); 
						}
                    }
				}

				if (sliderWorkload != null) {
					//sliderWorkload.maxValue = maxWorkloadValue;
				}
			}

	}

	public void pushWorkload(int i){
		Debug.Log ("pushed " + i.ToString());

		if (roundBasedGame.instance.playerMap.ContainsKey ((Department)i)) {
			roundBasedGame.instance.playerMap [(Department)i].pullWorkload (this);
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

	public List<float> workloadToGive;
	public List<float> workloadToTake;

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			if (isPlayer) {
				stream.SendNext (thisWorkloadValue);
				stream.SendNext ((int)myDepartment);
				stream.SendNext (blameValue);
				stream.SendNext (hasBlameSticker);
				for (int i = 0; i < workloadToGive.Count; i++) {
					stream.SendNext (workloadToGive[i]);
				}
				for (int i = 0; i < workloadToTake.Count; i++) {
					stream.SendNext (workloadToTake[i]);
				}

				for (int i = 0; i < workloadToGive.Count; i++) {
					workloadToGive [i] = 0;
				}
				for (int i = 0; i < workloadToTake.Count; i++) {
					workloadToTake [i] = 0;
				}
			}
		}
		else
		{
			if (!isPlayer) {
				
				thisWorkloadValue = (float)stream.ReceiveNext ();

				//Department oldDept = myDepartment;
				myDepartment = (Department)((int)stream.ReceiveNext ());
				/*
				if (oldDept != myDepartment) {
					roundBasedGame.instance.playerMap [oldDept] = null;
					if (roundBasedGame.instance.playerMap.ContainsKey (myDepartment)) {
						roundBasedGame.instance.playerMap [myDepartment] = this;
					} else {
						roundBasedGame.instance.playerMap.Add (myDepartment, this);
					}
				}*/

				blameValue = (float)stream.ReceiveNext ();
				hasBlameSticker = (bool)stream.ReceiveNext ();

				if (stream.Count == 12) {

					List<float> workloadReceived = new List<float> ();
					for (int i = 0; i < 4; i++) {
						workloadReceived.Add ((float)stream.ReceiveNext ());
					}
					List<float> workloadTakenFromMe = new List<float> ();
					for (int i = 0; i < 4; i++) {
						workloadTakenFromMe.Add ((float)stream.ReceiveNext ());
					}

					thisWorkloadValue += workloadReceived [(int)myDepartment];
					thisWorkloadValue -= workloadTakenFromMe [(int)myDepartment];

				}

				Debug.Log (name + " has Received from: " + info.sender.NickName);
			}
		}
	}
}
