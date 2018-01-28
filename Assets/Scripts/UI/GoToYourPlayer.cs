using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToYourPlayer : MonoBehaviour {

    public Transform transformButton;

    void Start () {
        this.transform.position = transformButton.position;
	}
}
