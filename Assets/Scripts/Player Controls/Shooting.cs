using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    //Private Variables
    private Button _shootingButton;
    private Targeting _targetingScript;
    private float _forceMultiplier = 2.0f;

    //Serialized Variables
    [SerializeField] private GameObject laserProjectile;

    //Public Variables
    public int _shootingBlaster;
    public Transform laserSpawnPoint_1;
    public Transform laserSpawnPoint_2;

    void Start()
    {
        _targetingScript = GetComponent<Targeting>();

        //establishing reference to the shooting button
        _shootingButton = GameObject.FindGameObjectWithTag("Button_Shoot").GetComponent<Button>();
        //subscribing the Shoot() function to the button on click event
        _shootingButton.onClick.AddListener(Shoot);
    }

    public void Shoot()
    {
        if (_targetingScript.targetedEnemy != null)
        {
            _shootingBlaster = Random.Range(1, 3);

            if (_shootingBlaster == 1)
            {
                Instantiate(laserProjectile, laserSpawnPoint_1.position, laserSpawnPoint_1.rotation * Quaternion.Euler(90.0f, 0.0f, 0.0f));
            }
            else if (_shootingBlaster == 2)
            {
                Instantiate(laserProjectile, laserSpawnPoint_2.position, laserSpawnPoint_2.rotation * Quaternion.Euler(90.0f, 0.0f, 0.0f));
            }

            //Debugging
            Debug.Log("Shot Enemy: " + _targetingScript.targetedEnemy);
        }
    }
}
