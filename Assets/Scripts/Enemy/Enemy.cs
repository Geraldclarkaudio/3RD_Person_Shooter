using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private CharacterController _controller;
    private Transform player;

    [SerializeField]
    private float _speed = 5f;

    private Vector3 _velocity;
    private float gravity = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(_controller.isGrounded == true)
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
}
