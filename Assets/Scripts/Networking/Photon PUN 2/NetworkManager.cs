using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public sealed class NetworkManager : MonoBehaviourPunCallbacks
{
    //Singleton pattern
    public static NetworkManager Instance { set; get; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        } else
        {
            Destroy(this.gameObject);
        }

        GameManager.OnGameStateChanged += LoadScene_1;
    }

    public void StartConnectToServer()
    {
        Debug.Log("Try to connect to the server...");

        //Connect to server
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to the server!");

        //Next up join a lobby
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Ready to join multiplayer session!");

        //Joining a room
        FindMatch();
    }

    private void FindMatch()
    {
        Debug.Log("Find a room ...");

        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateNewRoom();
    }

    private void CreateNewRoom()
    {
        int randomRoomNum = Random.Range(0, 9999);
        RoomOptions roomOptions = new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 5,
            PublishUserId = true
        };

        PhotonNetwork.CreateRoom("Contract #" + randomRoomNum, roomOptions);

        Debug.Log("Contract #" + randomRoomNum + " created!");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joining room!");

        //Update the GameState
        GameManager.Instance.UpdateGameState(GameState.MainMenu);
    }

    private void LoadScene_1(GameState state)
    {
        if (state == GameState.MainMenu)
        {
            PhotonNetwork.LoadLevel("Scene_1");

            //Update the GameState
            GameManager.Instance.UpdateGameState(GameState.FindPlayArea);
        }
    }
}
