using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerChaser : MonoBehaviour
{
    //Private Variables
    private NavMeshAgent _navMeshAgent;
    private Transform _playerPos;
    private float _threshold = 0.15f;
    private bool _chasing = true;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        ChasePlayer();
    }

    private void ChasePlayer()
    {
        if (_chasing == true)
        {
            _navMeshAgent.SetDestination(_playerPos.position);
        }

        Vector3 distToPlayer = transform.position - _playerPos.position;

        //Debug.Log("Magnitude of distToPlayer: " + distToPlayer.magnitude);

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
