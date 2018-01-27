using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAlive : MonoBehaviour
{
    private PhotonView photonView;
    public bool actionDoneThisRound;
    private Text lifeText;
    //private Button killButton;
    public GameObject manager;


    private void Start()
    {
        photonView = transform.GetComponent<PhotonView>();
        lifeText = GameObject.Find(StringCollection.LIFETEXT).GetComponent<Text>();
        //killButton = GameObject.Find(StringCollection.KILLBUTTON).GetComponent<Button>();
        actionDoneThisRound = false;
    }

    public void newRound()
    {
        actionDoneThisRound = false;
    }

    public void ClickToKillPlayer()
    {
        if (!actionDoneThisRound)
        {
            photonView.RPC("KillPlayer", PhotonTargets.Others);
            lifeText.text = StringCollection.PLAYERSTATUS + actionDoneThisRound;
        }
    }

    public void KillPlayer()
    {
        actionDoneThisRound = true;
    }

}
