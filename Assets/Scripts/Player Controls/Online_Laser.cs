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
    private GameObject _player;
    private GameObject[] _players;

    //Serialized Variables --> DON'T FORGET TO ASSIGN THESE
    [SerializeField] private PhotonView photonView;

    void Awake()
    {
        //This throws NullReference Erros for the secondary player, since on their game runtime _player is never assigned
        _players = GameObject.FindGameObjectsWithTag("Player");

        if (_players.Length == 1)
        {
            _player = _players[0];
        }
        else if (_players.Length > 1)
        {
            for (int i = 0; i < _players.Length; i++)
            {
                if (_players[i].GetComponent<PhotonView>().IsMine)
                {
                    _player = _players[i];
                }
            }
        }
    }

    void Start()
    {
        _laserRB = GetComponent<Rigidbody>();
        photonView = GetComponent<PhotonView>();

        //_player = GameObject.FindGameObjectWithTag("Player");

        _targetingScript = _player.GetComponent<Targeting>();
        _onlineShootingScript = _player.GetComponent<Online_Shooting>();
    }

    void FixedUpdate()
    {
        if (_targetingScript.targetedEnemy != null && _targetingScript.targetedEnemy.activeSelf == true)
        {
            transform.LookAt(_targetingScript.targetedEnemy.transform.position + new Vector3(0.0f, 0.15f, 0.0f));
        }

        if (_onlineShootingScript._shootingBlaster == 1)
        {
            _laserRB.AddForce(((_targetingScript.targetedEnemy.transform.position + new Vector3(0.0f, 0.15f, 0.0f)) - _onlineShootingScript.laserSpawnPoint_1.position) * _forceMultiplier, ForceMode.Force);
        }
        else if (_onlineShootingScript._shootingBlaster == 2)
        {
            _laserRB.AddForce(((_targetingScript.targetedEnemy.transform.position + new Vector3(0.0f, 0.15f, 0.0f)) - _onlineShootingScript.laserSpawnPoint_2.position) * _forceMultiplier, ForceMode.Force);
        }

        DestroyIfOutOfBounds();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //Make the enemy nav mesh agent stop
            collision.gameObject.GetComponent<PlayerChaser>().hasBeenShot = true;

            //Play shot enemy dying anim
            collision.gameObject.GetComponent<Animator>().SetTrigger("SetDying");

            //Set shot enemy inactive (without delay) DelayedToggleEnemy
            //collision.gameObject.GetComponent<EnemyToggler>().photonView.RPC("ToggleEnemy", RpcTarget.All, false);

            //Set shot enemy inactive (with 3 seconds delay)
            collision.gameObject.GetComponent<EnemyToggler>().photonView.RPC("DelayedToggleEnemy", RpcTarget.All);
        }

        //PhotonNetwork.Destroy(this.gameObject);
        photonView.RPC("NetworkDestroyLaser", RpcTarget.All);
    }

    private void DestroyIfOutOfBounds()
    {
        if (Vector3.Distance(_player.transform.position, transform.position) > 3.0f)
        {
            //PhotonNetwork.Destroy(this.gameObject);
            photonView.RPC("NetworkDestroyLaser", RpcTarget.All);
        }
    }

    [PunRPC]
    private void NetworkDestroyLaser()
    {
        Destroy(this.gameObject);
    }
}
