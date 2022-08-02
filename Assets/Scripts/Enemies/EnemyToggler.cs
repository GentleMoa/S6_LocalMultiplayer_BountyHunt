using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyToggler : MonoBehaviour
{
    //Public Variables
    public PhotonView photonView;

    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    [PunRPC]
    public void ToggleEnemy(bool boolean)
    {
        this.gameObject.SetActive(boolean);
    }
}
