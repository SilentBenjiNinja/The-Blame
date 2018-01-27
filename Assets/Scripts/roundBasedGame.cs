using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roundBasedGame : MonoBehaviour {

    private bool hasGameStarted = false;

    public float ROUNDTIME = 2;
    public float roundTimeElapsed;

    public int ROUNDSBETWEENWORKLOADS = 10;
    public int roundsElapsed;

    public PlayerActions playerActions;

    public float overallTimeDepleted;

    public List<PlayerActions> playersArray;
    public static roundBasedGame instance;

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
        overallTimeDepleted += Time.deltaTime;
        if (hasGameStarted)
        {
            roundTimeElapsed += Time.deltaTime;
            if (roundTimeElapsed >= ROUNDTIME)
            {
                roundTimeElapsed -= ROUNDTIME;
                foreach (PlayerActions player in playersArray)
                {
                    player.GetComponent<PlayerActions>().newRound();
                }
                roundsElapsed += 1;
                if (roundsElapsed >= ROUNDSBETWEENWORKLOADS)
                {
                    // ++++++++nextWorkload++++++++++++++++
                    roundsElapsed = 0;
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
}
