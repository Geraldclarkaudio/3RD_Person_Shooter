using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Controller Settings")]
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private float _jumpHeight = 8.0f;
    [SerializeField]
    private float _gravity = 20.0f;
    private Vector3 _velocity;
    private CharacterController _controller;

    [Header("Camera Settings")]
    [SerializeField]
    private float _cameraSensitivity = 2.0f;
    private Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;

        if(mainCam == null)
        {
            Debug.LogError("main cam is null");
        }

        _controller = GetComponent<CharacterController>();
        
        if(_controller == null)
        {
            Debug.LogError("Character Controller is Null");
        }

        //lock cursor and hide it
        //escape key to unlock and reshow
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        CameraRotation();

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void CalculateMovement()
    {
        if (_controller.isGrounded == true)
        {
            float horizontalMovement = Input.GetAxis("Horizontal");
            float verticalMovement = Input.GetAxis("Vertical");
            Vector3 direction = new Vector3(horizontalMovement, 0, verticalMovement);
            _velocity = direction *= _speed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                //jump
                _velocity.y = _jumpHeight;
            }
        }

        _velocity.y -= _gravity * Time.deltaTime;

        _velocity = transform.TransformDirection(_velocity);
        _controller.Move(_velocity * Time.deltaTime);
    }

    void CameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");


        Vector3 currentRotation = transform.localEulerAngles;
        currentRotation.y += mouseX * _cameraSensitivity;
        transform.localRotation = Quaternion.AngleAxis(currentRotation.y, Vector3.up);



        //apply mousey to camera roation x (look Up and Down)
        //clamp between 0 and 15
        Vector3 currentCamRot = mainCam.gameObject.transform.localEulerAngles;
        currentCamRot.x -= mouseY * _cameraSensitivity;
        currentCamRot.x = Mathf.Clamp(currentCamRot.x, 0, 26);
        mainCam.gameObject.transform.localRotation = Quaternion.AngleAxis(currentCamRot.x, Vector3.right);
    }
}
