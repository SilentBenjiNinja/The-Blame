using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	public AudioSource audioSource;
	public AudioClip loopClip;

	public static MusicManager instance;

	// Use this for initialization
	void Start () {

		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);
			StartCoroutine (PlayLoop ());
		} else {
			Destroy (gameObject);
		}

	}

	IEnumerator PlayLoop(){

		yield return new WaitForSeconds (audioSource.clip.length - audioSource.time);

		audioSource.clip = loopClip;
		audioSource.loop = true;
		audioSource.time = 0;
		audioSource.Play ();

	}

	// Update is called once per frame
	void Update () {
		
	}
}
