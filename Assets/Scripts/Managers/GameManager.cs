using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Giving the possibility to print logs into a text field in the canvas
    [SerializeField] private TMP_Text debugLogUI;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    //Singleton pattern
    public static GameManager Instance
    {
        get
        {
            return FindObjectOfType<GameManager>();
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        //For UIDebugger
        OnGameStateChanged += RefindUIDebuggerReference;
    }

    void Start()
    {
        //Find Reference to UIDebugger
        debugLogUI = GameObject.FindGameObjectWithTag("UIDebugger").GetComponentInChildren<TMP_Text>();

        UpdateGameState(GameState.Beginning);
    }

    public void UpdateGameState(GameState newState)
    {
        Debug.Log("Old State: " + State);

        State = newState;

        //Debugging
        Debug.Log("New State: " + State);
        if (debugLogUI != null)
        {
            debugLogUI.text = "Current GameState: " + State;
        }

        switch (newState)
        {
            case GameState.Beginning:
                break;
            case GameState.MainMenu:
                break;
            case GameState.FindPlayArea:
                break;
            case GameState.PlayAreaFound:
                break;
            case GameState.PrepareEnemySpawning:
                break;
            case GameState.BeginEnemySpawning:
                break;
            case GameState.Gameplay:
                break;
            case GameState.Ending:
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }

    private void RefindUIDebuggerReference(GameState state)
    {
        if (state == GameState.FindPlayArea)
        {
            Invoke("FindUIDebuggerReference", 0.2f);
        }
    }

    private void FindUIDebuggerReference()
    {
        //Find Reference to UIDebugger
        debugLogUI = GameObject.FindGameObjectWithTag("UIDebugger").GetComponentInChildren<TMP_Text>();

        OnGameStateChanged -= RefindUIDebuggerReference;
    }
}

//GameStates
public enum GameState
{
    Beginning,
    MainMenu,
    FindPlayArea,
    PlayAreaFound,
    PrepareEnemySpawning,
    BeginEnemySpawning,
    Gameplay,
    Ending
}
