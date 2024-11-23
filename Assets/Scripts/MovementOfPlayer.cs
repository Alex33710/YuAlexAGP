using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MovementOfPlayer : MonoBehaviour
{
    private CharacterController CC;
    public float w = 10f;
    public float g = -9.81f * 2;
    public float jumpH = 3f;

    public Transform groundCheck;
    public float GDist = 0.4f;
    public LayerMask GMask;

    Vector3 velocity;

    bool isGrounded;
    bool isMoving;
    bool isRunning;

    private Vector3 lastPosition = new Vector3(0f, 0f, 0f);

    void Start()
    {
        CC = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, GDist, GMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        //Player Movement
        CC.Move(move * w * Time.deltaTime);

        //Player Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //Currently Jumping
            velocity.y = Mathf.Sqrt(jumpH * -2f * g);
        }

        //Fall down
        velocity.y += g * Time.deltaTime;

        //Executing jump
        CC.Move(velocity * Time.deltaTime);

        if (lastPosition != gameObject.transform.position) 
        {
            isMoving = true;
        
        }
        else
        {
            isMoving = false;
        }

        lastPosition = gameObject.transform.position;
    }
}


