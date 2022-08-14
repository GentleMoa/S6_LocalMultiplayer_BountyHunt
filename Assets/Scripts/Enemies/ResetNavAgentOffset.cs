using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ResetNavAgentOffset : MonoBehaviour
{
    void Start()
    {
        //Resets the enemies' NavMeshAgent's Base Offset to 0, fixing hovering over the floor
        var _navMeshAgent = GetComponent<NavMeshAgent>().baseOffset = 0.0f;
    }
}
