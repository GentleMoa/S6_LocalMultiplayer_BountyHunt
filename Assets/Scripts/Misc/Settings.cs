using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    //Singleton pattern for easy access
    public static Settings Instance
    {
        get
        {
            return FindObjectOfType<Settings>();
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    //Reference Archive

    [Header("")]
    //For all Prefabs
    [Header("Prefabs")]

    //For Script: AR_LevelSpawner --> On "GameLogicHolder" GameObject, in "Scene_1"
    [Header ("AR_LevelSpawner")]
    public GameObject levelMap;
    public GameObject player;

    //For Script: EnemySpawner --> On "GameLogicHolder" GameObject, in "Scene_1"
    [Header("EnemySpawner")]
    public GameObject enemy;

    //For Script: Targeting --> On "IG11_Player" Prefab, spawned in "Scene_1"
    [Header ("Targeting")]
    public Material matEnemy;
    public Material matEnemyInLOS;

    [Header("")]
    //For all Audio Scripts
    [Header("Audio")]

    [Header("Player")]
    public AudioClip audio_robot_activateLaser;

    [Header("EnemySpawning")]
    public AudioClip audio_enemy_spawning;

    [Header("Laser")]
    public AudioClip[] audio_lasers_shot;

    [Header("Button_Quit")]
    public AudioClip audio_quitButton_sound;

    [Header("Buttons_Misc")]
    public AudioClip audio_startButton_sound;

    [Header("")]
    //For all Music Pieces
    [Header("Music")]

    [Header("MainMenu")]
    public AudioClip music_mainMenu;
}
