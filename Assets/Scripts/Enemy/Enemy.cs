using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Chase,
        Attack
    }

    [SerializeField]
    private EnemyState enemyState = EnemyState.Chase;//default state

    //Enemy stats
    private CharacterController _controller;
    [SerializeField]
    private float _speed = 5f;
    private Vector3 _velocity;
    private float gravity = 20.0f;

    //player reference
    private Transform player;
    private Health playerHealth;
    
    //CoolDown Attack
    private float _attackDelay = 1.5f;
    private float nextAttack = -1;
   

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<Health>();

        if(_controller == null || player == null || playerHealth == null)
        {
            Debug.LogError("Something is Null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if within range stop moving. 
        if(enemyState == EnemyState.Chase)
        {
            CalculateMovement();
        }

        if(enemyState == EnemyState.Attack)
        {
            //cool down implementation

            if(Time.time > nextAttack)
            {
                playerHealth.Damage(10);
                nextAttack = Time.time + _attackDelay;
            }
        }
    }

    void CalculateMovement()
    {
        if (_controller.isGrounded == true)
        {
            Vector3 direction = player.position - transform.position;
            direction.Normalize();//slows down
            direction.y = 0;//so it cant tilt toward me.
            transform.localRotation = Quaternion.LookRotation(direction);
            _velocity = direction * _speed;

        }
        _velocity.y -= gravity;
        _controller.Move(_velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            enemyState = EnemyState.Attack;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            enemyState = EnemyState.Chase;
        }
       
    }
}

