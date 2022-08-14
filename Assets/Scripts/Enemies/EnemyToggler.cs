using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyToggler : MonoBehaviour
{
    //Public Variables
    [HideInInspector]
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

    [PunRPC]
    public void DelayedToggleEnemy()
    {
        StartCoroutine(DisableEnemy(2.8f, false));
    }

    IEnumerator DisableEnemy(float delay, bool boolean)
    {
        yield return new WaitForSeconds(delay);

        //Disable Enemy code
        this.gameObject.SetActive(boolean);
    }
}
