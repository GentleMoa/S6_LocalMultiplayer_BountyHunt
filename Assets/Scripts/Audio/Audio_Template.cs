using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Audio_Template : MonoBehaviour
{
    //Serialized Variables (For some reason, GetComponent<> didn't work on these) --> DON'T FORGET TO ASSIGN THESE
    [SerializeField] private PhotonView _photonView;
    [SerializeField] private AudioSource _audioSource;

    //Private Variables
    //private AudioClip _audio_enemy_spawning;

    void OnEnable()
    {
        //Subscribe "RunRPC" function to AudioManager's Audio Event
        //AudioManager.Instance.AE_Enemy_Spawning += RunRPC_Audio_Enemy_Spawning;

        //Audio Condition (if the enemy gets enabled) --> Tell the AudioManager to shoot the Audio Event
        //AudioManager.Instance.ShootAudioEvent_Enemy_Spawning();
    }

    void Start()
    {
        //Get the AudioClip from the Settings
        //_audio_enemy_spawning = Settings.Instance.audio_enemy_spawning;
    }

    private void RunRPC_Audio_Enemy_Spawning()
    {
        //Run the RPC to enable the Audio playing for all players
        //_photonView.RPC("Play_Audio_Enemy_Spawning", RpcTarget.All);
    }

    [PunRPC]
    private void Play_Audio_Enemy_Spawning()
    {
        //Input AudioClip into AudioSource and play it
        //_audioSource.clip = _audio_enemy_spawning;
        _audioSource.Play();
    }

    void OnDisable()
    {
        //Unsubscribe "RunRPC" function from AudioManager's Audio Event
        //AudioManager.Instance.AE_Enemy_Spawning -= RunRPC_Audio_Enemy_Spawning;
    }

}
