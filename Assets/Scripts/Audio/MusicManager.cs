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
    private AudioClip[] _music_gameplay = new AudioClip[4]; //Scale this Array.Length according to Settings.Instance.music_gameplay.Length!

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

        //Get the AudioClip from the Settings
        System.Array.Copy(Settings.Instance.music_gameplay, _music_gameplay, Settings.Instance.music_gameplay.Length);

        //Subscribe the main function to the GameManager's OnGameStateChanged event, which controlls the game flow
        GameManager.OnGameStateChanged += PlayMusic;
    }


    private void PlayMusic(GameState state)
    {
        //Play MainMenu Music
        if (state == GameState.Beginning)
        {
            _audioSource.clip = _music_mainMenu;
            _audioSource.Play();
        }
        //Play Gameplay Music
        else if (state == GameState.PrepareEnemySpawning)
        {
            //Stopping the MainMenu Music
            _audioSource.loop = false;
            _audioSource.Stop();

            //Setting up and Starting the Gameplay Music
            _audioSource.volume = 0.2f;
            _audioSource.clip = _music_gameplay[0];
            _audioSource.Play();
        }
    }

    void Update()
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.clip = _music_gameplay[Random.Range(0, _music_gameplay.Length)];
            _audioSource.Play();
        }
    }

    void OnDisable()
    {
        //Unsubscribe the main function from the GameManager's OnGameStateChanged event, which controlls the game flow
        GameManager.OnGameStateChanged -= PlayMusic;
    }
}
