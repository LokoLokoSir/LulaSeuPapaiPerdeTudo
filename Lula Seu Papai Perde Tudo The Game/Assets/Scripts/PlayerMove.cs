using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("KeyBinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask isGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody playerRig;

    private void Start()
    {
        readyToJump = true;
        playerRig = GetComponent<Rigidbody>();
        playerRig.freezeRotation = true;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, isGround);

        MyInput();
        //SpeedControl();
        //gay

        if (grounded)
        {
            playerRig.drag = groundDrag;
        }else
        {
            playerRig.drag = 0;
        }
    }   

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
        {
            playerRig.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }else if (!grounded)
        {
            playerRig.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
        
    }

    //private void SpeedControl()
    //{
        //Vector3 flatVel = new Vector3(playerRig.velocity.x, 0f, playerRig.velocity.z);

        //if (flatVel.magnitude > moveSpeed)
        //{
            //Vector3 limitedVel = flatVel.normalized * moveSpeed;
            //playerRig.velocity = new Vector3(limitedVel.x, playerRig.velocity.x, limitedVel.z);
        //}
    //}

    private void Jump()
    {
        playerRig.velocity = new Vector3(playerRig.velocity.x, 0f, playerRig.velocity.z);

        playerRig.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}
