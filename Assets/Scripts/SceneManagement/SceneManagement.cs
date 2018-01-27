using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//SceneManager.LoadScene ("Basti_Scene", LoadSceneMode.Additive);
		//SceneManager.LoadScene ("Benji_Scene", LoadSceneMode.Additive);
		SceneManager.LoadScene ("James_Scene", LoadSceneMode.Additive);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
