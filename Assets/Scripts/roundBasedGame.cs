using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roundBasedGame : MonoBehaviour {

    public float ROUNDTIME = 2;
    public float roundTimeElapsed;
    public GameObject[] playersArray;

	void Start () {
        roundTimeElapsed = 0;
        playersArray = GameObject.FindGameObjectsWithTag("Player");
    }
	
	void Update () {
        if (roundTimeElapsed >= ROUNDTIME)
        {
            foreach (GameObject player in playersArray)
            {
                player.GetComponent<PlayerActions>().newRound();
            }
            roundTimeElapsed -= ROUNDTIME;
        }
        roundTimeElapsed += Time.deltaTime;
	}
}
