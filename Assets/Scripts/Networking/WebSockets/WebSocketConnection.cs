using UnityEngine;
using NativeWebSocket;
using TMPro;
using System.Text;
using System.IO;

public class WebSocketConnection : MonoBehaviour
{
    //Giving the possibility to print logs into a text field in the canvas
    [SerializeField] private TMP_Text debugLogUI;

    private WebSocket _webSocket;
    private string _serverUrl = "ws://bountyar.uber.space:42761/NodeJSServer"; // REPLACE [username] & [port] with yours
    private int _serverErrorCode;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    async void Start()
    {
        _webSocket = new WebSocket(_serverUrl);

        _webSocket.OnOpen += OnOpen;
        _webSocket.OnMessage += OnMessage;
        _webSocket.OnClose += OnClose;
        _webSocket.OnError += OnError;

        await _webSocket.Connect();
    }

    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        _webSocket.DispatchMessageQueue();
#endif
    }


    //Event Handlers
    private void OnOpen()
    {
        print("Connection opened");

        //Giving the possibility to print logs into a text field in the canvas
        debugLogUI.text = "Connection opened";

        Invoke("SendEmptyMessageToServer", 0.0f);
    }

    private void OnMessage(byte[] incomingBytes)
    {
        print("Message received");
    }

    private void OnClose(WebSocketCloseCode closeCode)
    {
        print($"Connection closed: {closeCode}");
    }

    private void OnError(string errorMessage)
    {
        print($"Connection error: {errorMessage}");
    }

    private async void OnApplicationQuit()
    {
        await _webSocket.Close();
    }


    //Function Archive
    private async void SendEmptyMessageToServer()
    {
        if (_webSocket.State  == WebSocketState.Open)
        {
            byte[] bytes = new byte[1] { 1 };
            await _webSocket.Send(bytes);
        }
    }

    //private void HandleIncomingData()
    //{
    //    // 1. Convert incoming byte[] to string:
    //    string incomingString = System.Text.Encoding.UTF8.GetString(incomingBytes);
    //
    //    // 2. Check wether incoming string is an int or not. If it is an int, it isn't JSON but a serverError.
    //    if (int.TryParse(incomingString, out _serverErrorCode))
    //    {
    //        //Handle serverError
    //        print($"Server Error: {_serverErrorCode}");
    //    }
    //    else
    //    {
    //        //Handle JSON
    //    }
    //
    //    // 3. After converting the byte[] data into JSON string, you can chekc for classType like this:
    //    var parsedJson = JSON.Parse(yourJsonString);
    //    string classType = N["classType"].Value;
    //
    //    if (classType == "PlayerPosition")
    //    {
    //        //Convert incoming JSON to object of type "PlayerPosition"
    //    }
    //    else /* if (classType == "...") */
    //    {
    //        //Check for other potential cases
    //    }
    //
    //}
}