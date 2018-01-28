using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class roundBasedGame : MonoBehaviour {

    private bool hasGameStarted = false;

    public float ROUNDTIME = 2;
    public float roundTimeElapsed;

    public int ROUNDSBETWEENWORKLOADS = 10;
    public int roundsElapsed;

    public float overallTimeDepleted;

    public List<PlayerActions> playersArray;
	public Dictionary<PlayerActions.Department, PlayerActions> playerMap = new Dictionary<PlayerActions.Department, PlayerActions>();
    public static roundBasedGame instance;

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
            if (playersArray.Count == 4)
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
