using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class PlayerInfo : MonoBehaviour
{
    //Private Variables
    private TMP_Text playerInfoUI;

    void Start()
    {
        playerInfoUI = GetComponentInChildren<TMP_Text>();
        playerInfoUI.text = "Player is master: " + PhotonNetwork.IsMasterClient;
    }

}
