using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void StartGame()
    {
        //Establish a connection to the server
        NetworkManager.Instance.StartConnectToServer();
    }
}
