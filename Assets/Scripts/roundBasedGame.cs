using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roundBasedGame : MonoBehaviour {

    private bool hasGameStarted = false;
    public float ROUNDTIME = 2;
    public float roundTimeElapsed;
    public float overallTimeDepleted;
    public List<PlayerActions> playersArray;
    public static roundBasedGame instance;

    private void Start()
    {
        overallTimeDepleted = 0;
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
        roundTimeElapsed = 0;        
    }
	
	void Update () {
        overallTimeDepleted += Time.deltaTime;
        if (hasGameStarted)
        {
            if (roundTimeElapsed >= ROUNDTIME)
            {
                foreach (PlayerActions player in playersArray)
                {
                    player.GetComponent<PlayerActions>().newRound();
                }
                roundTimeElapsed -= ROUNDTIME;
            }
            roundTimeElapsed += Time.deltaTime;
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
