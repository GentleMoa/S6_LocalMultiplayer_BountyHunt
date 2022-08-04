using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Audio_QuitButton : MonoBehaviour
{
    //Serialized Variables (For some reason, GetComponent<> didn't work on these) --> DON'T FORGET TO ASSIGN THESE
    [SerializeField] private AudioSource _audioSource;

    //Private Variables
    private AudioClip _audio_quitButton_sound;

    void Start()
    {
        //Subscribe function to AudioManager's Audio Event --> Do it in Start instead of OnEnable() because this object is in the same scene as the AudioManager from the get-go
        AudioManager.Instance.AE_QuitButton_Sound += Play_Audio_QuitButton_Sound;

        //Get the AudioClip from the Settings
        _audio_quitButton_sound = Settings.Instance.audio_quitButton_sound;
    }

    public void AudioCondition()
    {
        //Audio Condition (if the enemy gets enabled) --> Tell the AudioManager to shoot the Audio Event
        AudioManager.Instance.ShootAudioEvent_QuitButton_Sound();
    }

    private void Play_Audio_QuitButton_Sound()
    {
        //Input AudioClip into AudioSource and play it
        _audioSource.clip = _audio_quitButton_sound;
        _audioSource.Play();
    }

    void OnDisable()
    {
        //Unsubscribe function from AudioManager's Audio Event
        AudioManager.Instance.AE_QuitButton_Sound -= Play_Audio_QuitButton_Sound;
    }

}
