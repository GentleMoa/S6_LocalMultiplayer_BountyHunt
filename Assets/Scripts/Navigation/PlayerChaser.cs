using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class PlayerChaser : MonoBehaviour
{
    //Private Variables
    private NavMeshAgent _navMeshAgent;
    private float _threshold = 0.15f;
    private bool _chasing = true;

    //Serialized Variables
    [SerializeField] private Transform _playerPos;
    [SerializeField] private GameObject[] _players;


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
        if (_playerPos != null)
        {
            if (_chasing == true)
            {
                _navMeshAgent.SetDestination(_playerPos.position);
            }

            Vector3 distToPlayer = transform.position - _playerPos.position;

            if (distToPlayer.magnitude < _threshold)
            {
                _chasing = false;
            }
            else
            {
                _chasing = true;
            }
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
