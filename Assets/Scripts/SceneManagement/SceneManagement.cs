using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour {

	//-------------------------
	//-------------------------
	#region LoadOtherScenes

	public List<string> scenesToLoad;

	// Use this for initialization
	void Start () {

		for (int i = 0; i < scenesToLoad.Count; i++) {
			SceneManager.LoadSceneAsync (scenesToLoad[i], LoadSceneMode.Additive);
			Scene scene = SceneManager.GetSceneByName (scenesToLoad[i]);
			loadingScenes.Add (scene);
		}
	}

	private bool[] RemoveCamDLight(GameObject gameObject, bool[] found){

		if (found [0] && found [1]) {
			return found;
		}

		if (!found [0]) {
			Camera cam = gameObject.GetComponent<Camera> ();
			if (cam != null) {
				found [0] = true;

				Destroy (gameObject);

				/*
				AudioListener audioListener = gameObject.GetComponent<AudioListener> ();
				if (audioListener != null) {
					Destroy (audioListener);
				}

				FlareLayer flare = gameObject.GetComponent<FlareLayer> ();
				if (flare != null) {
					Destroy (flare);
				}

				Destroy (cam);
				*/

			}
		}

		if (!found [1]) {
			Light dLight = gameObject.GetComponent<Light> ();
			if (dLight != null) {
				found [1] = true;
				//Destroy (dLight);
				Destroy(gameObject);
			}
		}

		if (found [0] && found [1]) {
			return found;
		} else {
			for (int i = 0; i < gameObject.transform.childCount; i++) {
				found = RemoveCamDLight (gameObject.transform.GetChild (i).gameObject, found);
			}
		}

		return found;

	}

	#endregion
	//-------------------------
	//-------------------------
	#region Cleanup Other Scenes

	private List<Scene> loadingScenes = new List<Scene>();

	// Update is called once per frame
	void Update () {
		for (int i = 0; i < loadingScenes.Count; i++) {
			if (loadingScenes [i].isLoaded) {
				Scene loadedScene = loadingScenes [i];
				CleanUpScene (loadedScene);
				loadingScenes.RemoveAt (i);
				i--;
			}
		}
	}

	private void CleanUpScene(Scene scene){
		bool foundCam = false;
		bool foundDLight = false;
		GameObject[] rootObjs = scene.GetRootGameObjects ();
		if (rootObjs != null) {
			for (int i = 0; i < rootObjs.Length && !foundCam && !foundDLight; i++) {
				bool[] result = RemoveCamDLight(rootObjs [i], new bool[]{foundCam,foundDLight});
				if (result [0]) {
					foundCam = true;
				}
				if (result [1]) {
					foundDLight = true;
				}
			}
		}
	}

	#endregion
	//-------------------------
	//-------------------------
}
