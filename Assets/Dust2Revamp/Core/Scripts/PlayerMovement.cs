using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class PlayerMovement : MonoBehaviour
{
    PlayerManager playerManager;
    public CharacterController controller;

    float speed;
    public float runSpeed = 24f;
    public float walkSpeed = 12f;

    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    Vector3 oldPos;

    private void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        oldPos = transform.position;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            speed = runSpeed;
            playerManager.animator.SetBool("Run", true);
            playerManager.animator.SetBool("Walk", false);
        }
        else
        {
            speed = walkSpeed;
            playerManager.animator.SetBool("Run", false);
            playerManager.animator.SetBool("Walk", true);
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if(oldPos == transform.position)
        {
            playerManager.animator.SetBool("Walk", false);
            playerManager.animator.SetBool("Run", false);
            playerManager.animator.SetBool("Idle", true);
        }

        oldPos = transform.position;

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight *-2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
