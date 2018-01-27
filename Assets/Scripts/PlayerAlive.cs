using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAlive : MonoBehaviour
{
    private PhotonView photonView;
    public bool isAlive = true;
    private Text lifeText;
    private Button killButton;


    private void Start()
    {
        photonView = transform.GetComponent<PhotonView>();
        lifeText = GameObject.Find(StringCollection.LIFETEXT).GetComponent<Text>();
        killButton = GameObject.Find(StringCollection.KILLBUTTON).GetComponent<Button>();
        isAlive = true;
    }

    public void ClickToKillPlayer()
    {
        
        photonView.RPC("KillPlayer", PhotonTargets.Others);
        lifeText.text = StringCollection.PLAYERSTATUS + isAlive;
    }

    public void KillPlayer()
    {
        isAlive = false;
    }

}
