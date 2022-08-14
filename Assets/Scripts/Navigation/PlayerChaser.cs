using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class PlayerChaser : MonoBehaviour
{
    //Private Variables
    private NavMeshAgent _navMeshAgent;

    //Serialized Variables
    [SerializeField] private Transform _playerPos;
    [SerializeField] private GameObject[] _players;

    //Public Variables
    public bool hasBeenShot = false;

    private void OnEnable()
    {
        hasBeenShot = false;
    }

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        FindPlayerReference();
        ChasePlayer();
    }

    private void ChasePlayer()
    {
        if (hasBeenShot == false)
        {
            if (_playerPos != null)
            {
                _navMeshAgent.isStopped = false;
                _navMeshAgent.SetDestination(_playerPos.position);
            }
        }
        else if (hasBeenShot == true)
        {
            _navMeshAgent.isStopped = true;
        }
    }

    private void FindPlayerReference()
    {
        if (_playerPos == null)
        {
            //Player reference gathering
            _players = GameObject.FindGameObjectsWithTag("Player");

            for (int i = 0; i < _players.Length; i++)
            {
                if (_players[i].GetComponent<PhotonView>().IsMine)
                {
                    _playerPos = _players[i].GetComponent<Transform>();
                }
            }
        }
    }
}
