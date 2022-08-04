using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Audio Events named by this convention: "AudioEvent" + "Audio Source Gameobject" + "Audio Category" (if present) + "Audio Specification"
    //For example: "AE_Player_Robot_ActivateLaser" ---> "AE" (marking it as Audio Event), "Player" (the gameobject which will play the sound), "Robot" (the sound category that will be played, if there is one), "ActivateLaser" (the specific sound that will be player)
    public event Action AE_Player_Robot_ActivateLaser;
    public event Action AE_Enemy_Spawning;
    public event Action AE_Laser_Shot;
    public event Action AE_QuitButton_Sound;
    public event Action AE_StartButton_Sound;

    //Singleton Pattern!
    public static AudioManager Instance { set; get; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void ShootAudioEvent_Player_Robot_ActivateLaser()
    {
        AE_Player_Robot_ActivateLaser();
    } 

    public void ShootAudioEvent_Enemy_Spawning()
    {
        AE_Enemy_Spawning();
    }

    public void ShootAudioEvent_Laser_Shot()
    {
        AE_Laser_Shot();
    }

    public void ShootAudioEvent_QuitButton_Sound()
    {
        AE_QuitButton_Sound();
    }

    public void ShootAudioEvent_StartButton_Sound()
    {
        AE_StartButton_Sound();
    }
}
