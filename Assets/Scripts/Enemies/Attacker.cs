using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //For Debugging
            //Debug.Log(gameObject.name + " is next to the player!");

            //Attack the player
            GetComponent<Animator>().SetTrigger("SetAttack");
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Attack the player
            GetComponent<Animator>().SetTrigger("SetAttack");
        }
    }
}
