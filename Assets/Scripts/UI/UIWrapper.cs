using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIWrapper : MonoBehaviour {
	
	//-------------------------
	//-------------------------

	public C FindComponent<C>(Transform transform) where C : Component {

		C c = transform.gameObject.GetComponent<C> ();
		for (int i = 0; i < transform.childCount && c == null; i++) {
			c = FindComponent<C>(transform.GetChild (i));
		}

		return c;
	}

	//-------------------------
	//-------------------------
	#region PunBasics-Launcher

	private ExitGames.Demos.DemoAnimator.Launcher launcher = null;

	private void FindPunBasics_Launcher(){
		Scene punBasicsLauncher = SceneManager.GetSceneByName ("PunBasics-Launcher");

		GameObject[] roots = punBasicsLauncher.GetRootGameObjects ();
		for (int i = 0; i < roots.Length && launcher==null; i++) {
			launcher = FindComponent<ExitGames.Demos.DemoAnimator.Launcher> (roots [i].transform);
		}
	}

	public void Launcher_Connect(){

		if (launcher == null) {
			FindPunBasics_Launcher ();
		}

		if (launcher != null) {
			launcher.Connect ();
		}

	}

	#endregion
	//-------------------------
	//-------------------------
	#region Benji-scene

	private Slider blameSlider;

	private void FindBenji_Scene(){
		Scene benjiScene = SceneManager.GetSceneByName ("Benji_Scene");

		GameObject[] roots = benjiScene.GetRootGameObjects ();
		for (int i = 0; i < roots.Length && blameSlider==null; i++) {
			Transform sliderTF = roots [i].transform.Find ("BlameMeter");
			if (sliderTF != null && sliderTF.gameObject != null) {
				Slider slider = sliderTF.GetComponent<Slider> ();
				if (slider != null) {
					blameSlider = slider;
				}
			}
		}
	}

	public Slider GetBlameSlider(){

		if (blameSlider == null) {
			FindBenji_Scene ();
		}

		if (blameSlider != null) {
			return blameSlider;
		}


		return null;
	}


	#endregion
	//-------------------------
	//-------------------------


}
