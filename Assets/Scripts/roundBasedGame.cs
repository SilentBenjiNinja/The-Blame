using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class roundBasedGame : MonoBehaviour {

    private bool hasGameStarted = false;
	public bool HasGameStarted{
		get{ 
			return hasGameStarted;
		}
	}

	public int numPlayersToStart = 4;

    public float ROUNDTIME = 2;
    public float roundTimeElapsed;

    public int ROUNDSBETWEENWORKLOADS = 10;
    public int roundsElapsed;


    public float overallTimeDepleted;

    public List<PlayerActions> playersArray;
	public List<PlayerActions> playersArrayEver = new List<PlayerActions>();
	public Dictionary<PlayerActions.Department, PlayerActions> playerMap = new Dictionary<PlayerActions.Department, PlayerActions>();
    
	public static roundBasedGame instance;
	public static int instanceCount = 0;

    public float WORKLOADAUTOVALUE = 5;

    public Text textTimeDepleted;
    public Slider sliderUntilAutoWorkload;
    public Text textUntilAutoWorkload;

    private void Awake()
    {
        sliderUntilAutoWorkload.interactable = false;

        sliderUntilAutoWorkload.maxValue = ROUNDTIME * ROUNDSBETWEENWORKLOADS;
        textUntilAutoWorkload.text = (ROUNDTIME * ROUNDSBETWEENWORKLOADS).ToString();
    }

    private void Start()
    {
		instanceCount++;
		playersArray = new List<PlayerActions> ();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void StartGame () {
        hasGameStarted = true;
        roundTimeElapsed = 0;
        overallTimeDepleted = 0;
        roundsElapsed = 0;
    }
	
	void Update () {
        
		if (playersArray != null) {
			for (int i = 0; i < playersArray.Count; i++) {				
				if (playersArray [i] != null && !playersArrayEver.Contains (playersArray [i])) {
					playersArrayEver.Add (playersArray [i]);
				}
			}

			playersArray.Clear ();
			for (int i = 0; i < playersArrayEver.Count; i++) {
				if (playersArrayEver [i] != null) {
					PhotonView pv = playersArrayEver [i].GetComponent<PhotonView> ();

					if (pv != null) {
						int endID = pv.viewID % 1000;
						if (endID == instanceCount) {

							playersArray.Add (playersArrayEver [i]);
						}
					}
				} else {
					playersArrayEver.RemoveAt (i);
					i--;
				}
			}
		}

        if (hasGameStarted)
        {
			overallTimeDepleted += Time.deltaTime;

            roundTimeElapsed += Time.deltaTime;
            if (roundTimeElapsed >= ROUNDTIME)
            {
                roundTimeElapsed -= ROUNDTIME;
                roundsElapsed += 1;
                foreach (PlayerActions player in playersArray)
                {
                    player.GetComponent<PlayerActions>().newRound();
                }
                if (roundsElapsed >= ROUNDSBETWEENWORKLOADS)
                {
                    roundsElapsed -= ROUNDSBETWEENWORKLOADS;
                    foreach (PlayerActions player in playersArray)
                    {
                        player.plusWorkload(WORKLOADAUTOVALUE);
                    }
                }
            }
        }
        else
        {
            if(playersArray == null)
            {
                playersArray = new List<PlayerActions>();
            }
            if (playersArray.Count == numPlayersToStart)
            {
                StartGame();
            }
        }
	}

    private void LateUpdate()
    {
        sliderUntilAutoWorkload.value = roundsElapsed * ROUNDTIME + roundTimeElapsed;
        textTimeDepleted.text = (overallTimeDepleted - (overallTimeDepleted % 0.1)).ToString();
        textUntilAutoWorkload.text = Mathf.Ceil(((ROUNDSBETWEENWORKLOADS - roundsElapsed) * ROUNDTIME) - roundTimeElapsed).ToString();
    }
}
