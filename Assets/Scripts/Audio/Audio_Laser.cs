using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Audio_Laser : MonoBehaviour
{
    //Serialized Variables (For some reason, GetComponent<> didn't work on these) --> DON'T FORGET TO ASSIGN THESE
    [SerializeField] private PhotonView _photonView;
    [SerializeField] private AudioSource _audioSource;

    //Private Variables
    private AudioClip[] _audio_lasers_shot = new AudioClip[4]; //Scale this Array.Length according to Settings.Instance.audio_lasers_shot.Length!

    void OnEnable()
    {
        //Get the AudioClips from the Settings
        System.Array.Copy(Settings.Instance.audio_lasers_shot, _audio_lasers_shot, Settings.Instance.audio_lasers_shot.Length);

        //Subscribe "RunRPC" function to AudioManager's Audio Event
        AudioManager.Instance.AE_Laser_Shot += RunRPC_Audio_Laser_Shot;

        //Audio Condition (if the enemy gets enabled) --> Tell the AudioManager to shoot the Audio Event
        AudioManager.Instance.ShootAudioEvent_Laser_Shot();
    }

    private void RunRPC_Audio_Laser_Shot()
    {
        //Run the RPC to enable the Audio playing for all players
        _photonView.RPC("Play_Audio_Laser_Shot", RpcTarget.All);
    }

    [PunRPC]
    private void Play_Audio_Laser_Shot()
    {
        //Input AudioClip into AudioSource and play it
        _audioSource.PlayOneShot(_audio_lasers_shot[Random.Range(0, _audio_lasers_shot.Length)]);
    }

    void OnDisable()
    {
        //Unsubscribe "RunRPC" function from AudioManager's Audio Event
        AudioManager.Instance.AE_Laser_Shot -= RunRPC_Audio_Laser_Shot;
    }

}
