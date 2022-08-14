using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyPool : MonoBehaviour
{
    //Public Variables
    public static EnemyPool SharedInstance;
    public List<GameObject> pooledObjects;

    //Serialized Variables
    [SerializeField] int poolSize;

    //Private Variables
    [SerializeField] private GameObject _enemy;

    void Awake()
    {
        SharedInstance = this;
        GameManager.OnGameStateChanged += EstablishEnemyPool;
    }

    private void EstablishEnemyPool(GameState state)
    {
        if (state == GameState.PlayAreaFound)
        {
            //For Online Multiplayer
            if (PhotonNetwork.InRoom /* && PhotonNetwork.IsMasterClient */)
            {
                pooledObjects = new List<GameObject>();
                GameObject _go;
                for (int i = 0; i < poolSize; i++)
                {
                    //Find Online_PlayArea
                    var onlinePlayAreaTransform = GameObject.FindGameObjectWithTag("Online_PlayArea").GetComponent<Transform>();

                    _go = PhotonNetwork.Instantiate("Online_Enemy_Clone", onlinePlayAreaTransform.position /* + new Vector3(0.0f, 0.1f, 0.0f) */, Quaternion.identity);
                    //_go.SetActive(false);
                    _go.GetComponent<EnemyToggler>().photonView.RPC("ToggleEnemy", RpcTarget.All, false);
                    pooledObjects.Add(_go);
                }
            }
            //For Offline Singleplayer
            else if (!PhotonNetwork.InRoom)
            {
                _enemy = Settings.Instance.enemy;
            
                pooledObjects = new List<GameObject>();
                GameObject _go;
                for (int i = 0; i < poolSize; i++)
                {
                    _go = Instantiate(_enemy);
                    _go.SetActive(false);
                    pooledObjects.Add(_go);
                }
            }
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < poolSize; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    void OnDisable()
    {
        GameManager.OnGameStateChanged -= EstablishEnemyPool;
    }
}
