using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Laser : MonoBehaviour
{
    //Private Variables
    private Rigidbody _laserRB;
    private GameObject _player;
    private Targeting _targetingScript;
    private Shooting _shootingScript;
    private float _forceMultiplier = 4.0f;

    void Start()
    {
        _laserRB = GetComponent<Rigidbody>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _targetingScript = _player.GetComponent<Targeting>();
        _shootingScript = _player.GetComponent<Shooting>();
    }

    void FixedUpdate()
    {
        transform.LookAt(_targetingScript.targetedEnemy.transform);

        if (_shootingScript._shootingBlaster == 1)
        {
            _laserRB.AddForce((_targetingScript.targetedEnemy.transform.position - _shootingScript.laserSpawnPoint_1.position) * _forceMultiplier, ForceMode.Force);
        }
        else if (_shootingScript._shootingBlaster == 2)
        {
            _laserRB.AddForce((_targetingScript.targetedEnemy.transform.position - _shootingScript.laserSpawnPoint_2.position) * _forceMultiplier, ForceMode.Force);
        }

        DestroyIfOutOfBounds();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.SetActive(false);
        }

        Destroy(this.gameObject);
    }

    private void DestroyIfOutOfBounds()
    {
        if (Vector3.Distance(_player.transform.position, transform.position) > 3.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
