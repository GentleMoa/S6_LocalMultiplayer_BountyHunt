using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    //Private Variables
    private Button _shootingButton;

    //Serialized Variables
    [SerializeField] private Targeting targetingScript;

    void Start()
    {
        //establishing reference to the shooting button
        _shootingButton = GameObject.FindGameObjectWithTag("Button_Shoot").GetComponent<Button>();
        //subscribing the Shoot() function to the button on click event
        _shootingButton.onClick.AddListener(Shoot);
    }

    public void Shoot()
    {
        //Shoot the target (closest enemy in line of sight) --> Instead of just disabling the shot enemy hit it with a bullet and deal dmg
        targetingScript.targetedEnemy.SetActive(false);

        //Debugging
        Debug.Log("Targeted Enemy: " + targetingScript.targetedEnemy);
    }
}
