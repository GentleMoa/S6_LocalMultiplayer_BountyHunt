using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MusicManager : MonoBehaviour
{
    //Serialized Variables (For some reason, GetComponent<> didn't work on these) --> DON'T FORGET TO ASSIGN THESE
    [SerializeField] private AudioSource _audioSource;
    //[SerializeField] private PhotonView _photonView;

    //Private Variables
    private AudioClip _music_mainMenu;

    //Singleton Pattern!
    public static MusicManager Instance { set; get; }

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

        //Get the AudioClip from the Settings
        _music_mainMenu = Settings.Instance.music_mainMenu;

        //Subscribe the main function to the GameManager's OnGameStateChanged event, which controlls the game flow
        GameManager.OnGameStateChanged += PlayMusic;
    }

    //void Start()
    //{
    //    //Get the AudioClip from the Settings
    //    _music_mainMenu = Settings.Instance.music_mainMenu;
    //
    //    //Subscribe the main function to the GameManager's OnGameStateChanged event, which controlls the game flow
    //    GameManager.OnGameStateChanged += PlayMusic;
    //}

    private void PlayMusic(GameState state)
    {
        //Play MainMenu Music
        if (state == GameState.Beginning)
        {
            _audioSource.clip = _music_mainMenu;
            _audioSource.Play();
        }

        ////Play Randomized Gameplay Music
        //if (state == GameState.FindPlayArea)
        //{
        //    //Randomize Gameplay Music
        //}
    }

    void OnDisable()
    {
        //Unsubscribe the main function from the GameManager's OnGameStateChanged event, which controlls the game flow
        GameManager.OnGameStateChanged -= PlayMusic;
    }
}
