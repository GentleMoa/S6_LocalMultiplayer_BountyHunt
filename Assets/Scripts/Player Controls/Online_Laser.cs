using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody))]
public class Online_Laser : MonoBehaviour
{
    //Private Variables
    private Rigidbody _laserRB;
    public Targeting _targetingScript;
    private Online_Shooting _onlineShootingScript;
    private float _forceMultiplier = 4.0f;

    //Serialized Variables
    [SerializeField] private GameObject _player;

    void Start()
    {
        _laserRB = GetComponent<Rigidbody>();
        _player = GameObject.FindGameObjectWithTag("Player");

        _targetingScript = _player.GetComponent<Targeting>();
        _onlineShootingScript = _player.GetComponent<Online_Shooting>();
    }

    void FixedUpdate()
    {
        if (_targetingScript.targetedEnemy != null && _targetingScript.targetedEnemy.activeSelf == true)
        {
            transform.LookAt(_targetingScript.targetedEnemy.transform);
        }
        //else
        //{
        //    Destroy(this.gameObject);
        //}

        if (_onlineShootingScript._shootingBlaster == 1)
        {
            _laserRB.AddForce((_targetingScript.targetedEnemy.transform.position - _onlineShootingScript.laserSpawnPoint_1.position) * _forceMultiplier, ForceMode.Force);
        }
        else if (_onlineShootingScript._shootingBlaster == 2)
        {
            _laserRB.AddForce((_targetingScript.targetedEnemy.transform.position - _onlineShootingScript.laserSpawnPoint_2.position) * _forceMultiplier, ForceMode.Force);
        }

        DestroyIfOutOfBounds();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.SetActive(false);
        }

        PhotonNetwork.Destroy(this.gameObject);
    }

    private void DestroyIfOutOfBounds()
    {
        if (Vector3.Distance(_player.transform.position, transform.position) > 3.0f)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
    }
}
