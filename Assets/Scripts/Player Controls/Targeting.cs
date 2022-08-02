using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations.Rigging;
//using System.Linq;

public class Targeting : MonoBehaviour
{
    //Private Variables
    private Vector3 _targetDirection;
    private Material _matEnemy;
    private Material _matEnemyInLOS;
    //private Material _matEnemyTargeted;
    private Transform _ig11_raycastOrign;
    //private Transform _lastSavedTransform;
    //private float _lerpValue;
    //private bool _enemyRegistered = false;
    private bool _torsoReset;

    //Serialized Variables
    [SerializeField] private List<GameObject> enemiesInLOS = new List<GameObject>();
    [SerializeField] private Transform torsoLookAtEnemy_Transform;
    [SerializeField] private Transform torsoDefaultRotation;
    //[SerializeField] private float lerpDuration = 1.0f;
    [SerializeField] private Transform leftHandLookAtEnemy_Transform;
    [SerializeField] private Transform rightHandLookAtEnemy_Transform;
    [SerializeField] private ChainIKConstraint leftHandIKConstraint;
    [SerializeField] private ChainIKConstraint rightHandIKConstraint;

    //Public Variables
    public GameObject targetedEnemy;

    void Start()
    {
        _matEnemy = Settings.Instance.matEnemy;
        _matEnemyInLOS = Settings.Instance.matEnemyInLOS;
        //_matEnemyTargeted = Settings.Instance.matEnemyTargeted;

        _ig11_raycastOrign = GameObject.FindGameObjectWithTag("IG11_RaycastOrigin").GetComponent<Transform>();
    }

    void Update()
    {
        CalculateTargetsInLineOfSight();
        CalculateClosestEnemy();
        TargetingUpperBody();
        TargetingHands();
    }

    private void CalculateTargetsInLineOfSight()
    {
        foreach (GameObject enemy in EnemyPool.SharedInstance.pooledObjects)
        {
            //Get the respective enemy's transform
            var enemyTransform = enemy.GetComponent<Transform>();

            RaycastHit hit;
            _targetDirection = (enemy.transform.position - _ig11_raycastOrign.position).normalized;

            if (Physics.Raycast(_ig11_raycastOrign.position, _targetDirection, out hit))
            {
                if (hit.transform == enemyTransform)
                {
                    if (!enemiesInLOS.Contains(hit.transform.gameObject))
                    {
                        enemiesInLOS.Add(hit.transform.gameObject);
                        UpdateEnemiesMat(enemy.GetComponent<Renderer>(), _matEnemyInLOS);
                    }
                }
                else
                {
                    if (enemiesInLOS.Contains(enemy))
                    {
                        enemiesInLOS.Remove(enemy);
                        UpdateEnemiesMat(enemy.GetComponent<Renderer>(), _matEnemy);
                    }
                }
            }

            //For Debugging
            Debug.DrawRay(_ig11_raycastOrign.position, _targetDirection, Color.red);
        }
    }

    private void CalculateClosestEnemy()
    {
        var shortestDistance = Mathf.Infinity;

        for (int i = 0; i < enemiesInLOS.Count; i++)
        {
            var distance = Vector3.Distance(_ig11_raycastOrign.position, enemiesInLOS[i].transform.position);

            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                targetedEnemy = enemiesInLOS[i];

                //put a crosshair over that enemy
                targetedEnemy.GetComponentInChildren<Image>().enabled = true;
            }
            else
            {
                //remove the crosshair over that enemy
                enemiesInLOS[i].GetComponentInChildren<Image>().enabled = false;
            }
        }
    }

    private void TargetingUpperBody()
    {
        if (targetedEnemy != null && targetedEnemy.activeSelf == true)
        {
            //Look at the targeted enemy
            torsoLookAtEnemy_Transform.LookAt(targetedEnemy.transform);

            //_lastSavedTransform = torsoLookAtEnemy_Transform;
            //_enemyRegistered = true;
            _torsoReset = false;
        }
        else
        {
            if (_torsoReset == false)
            {
                torsoLookAtEnemy_Transform.rotation = torsoDefaultRotation.rotation * Quaternion.Euler(0.0f, 90.0f, 180.0f);
                _torsoReset = true;
            }

            ////Lerp back to default rotation ---> NOT WORKING FOR SOME REASON
            //if (_enemyRegistered == true)
            //{
            //    _lerpValue += Time.deltaTime / lerpDuration;
            //    torsoLookAtEnemy_Transform.rotation = Quaternion.Lerp(_lastSavedTransform.rotation, torsoDefaultRotation.rotation, _lerpValue);
            //}
        }
    }

    private void TargetingHands()
    {
        if (targetedEnemy != null && targetedEnemy.activeSelf == true)
        {
            leftHandIKConstraint.weight = 1.0f;
            rightHandIKConstraint.weight = 1.0f;
        }
        else
        {
            leftHandIKConstraint.weight = 0.0f;
            rightHandIKConstraint.weight = 0.0f;
        }
    }

    private void UpdateEnemiesMat(Renderer rnd, Material mat)
    {
        if (rnd.material != mat)
        {
            rnd.material = mat;
        }
    }
}
