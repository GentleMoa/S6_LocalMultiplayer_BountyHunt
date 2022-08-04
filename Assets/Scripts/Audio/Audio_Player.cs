using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Audio_Player : MonoBehaviour
{
    //Serialized Variables (For some reason, GetComponent<> didn't work on these) --> DON'T FORGET TO ASSIGN THESE
    [SerializeField] private PhotonView _photonView;
    [SerializeField] private AudioSource _audioSource;

    //Private Variables
    private AudioClip _audio_robot_activateLaser;

    void OnEnable()
    {
        //Subscribe "RunRPC" function to AudioManager's Audio Event
        AudioManager.Instance.AE_Player_Robot_ActivateLaser += RunRPC_Audio_Robot_ActivateLaser;
    }

    void Start()
    {
        //Get the AudioClip from the Settings
        _audio_robot_activateLaser = Settings.Instance.audio_robot_activateLaser;

        //Audio Condition, for example purpose an easy one --> Tell the AudioManager to shoot the Audio Event
        if (this.gameObject.activeSelf == true)
        {
            AudioManager.Instance.ShootAudioEvent_Player_Robot_ActivateLaser();
        }
    }

    private void RunRPC_Audio_Robot_ActivateLaser()
    {
        //Run the RPC to enable the Audio playing for all players
        _photonView.RPC("Play_Audio_Robot_ActivateLaser", RpcTarget.All);
    }

    [PunRPC]
    private void Play_Audio_Robot_ActivateLaser()
    {
        //Input AudioClip into AudioSource and play it
        _audioSource.clip = _audio_robot_activateLaser;
        _audioSource.Play();
    }

    void OnDisable()
    {
        //Unsubscribe "RunRPC" function from AudioManager's Audio Event
        AudioManager.Instance.AE_Player_Robot_ActivateLaser -= RunRPC_Audio_Robot_ActivateLaser;
    }
}
