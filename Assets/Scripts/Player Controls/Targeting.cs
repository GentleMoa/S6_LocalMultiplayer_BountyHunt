using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System.Linq;

public class Targeting : MonoBehaviour
{
    //Private Variables
    private Vector3 _targetDirection;
    private Material _matEnemy;
    private Material _matEnemyInLOS;
    private Material _matEnemyTargeted;
    private Transform _ig11_raycastOrign;

    //Serialized Variables
    [SerializeField] private List<GameObject> enemiesInLOS = new List<GameObject>();
    [SerializeField] private Transform _lookAtEnemy_Transform;

    //Public Variables
    public GameObject targetedEnemy;

    void Start()
    {
        _matEnemy = Settings.Instance.matEnemy;
        _matEnemyInLOS = Settings.Instance.matEnemyInLOS;
        _matEnemyTargeted = Settings.Instance.matEnemyTargeted;

        _ig11_raycastOrign = GameObject.FindGameObjectWithTag("IG11_RaycastOrigin").GetComponent<Transform>();
        //_lookAtEnemy_Transform = GameObject.FindGameObjectWithTag("LookAt_TargetedEnemy_Obj").GetComponent<Transform>();
    }

    void Update()
    {
        CalculateTargetsInLineOfSight();
        CalculateClosestEnemy();
        TargetingUpperBody();
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
        if (targetedEnemy != null)
        {
            _lookAtEnemy_Transform.LookAt(targetedEnemy.transform);
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
