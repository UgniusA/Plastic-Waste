using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isGrounded;
    public GameObject groundCheck;
    public CharacterController controller;
    private int speed = 5;
    public int sprintSpeed = 7;
    public int walkSpeed = 5;
    public float gravity = 10f;
    public float groundDistance = 0.4f;
    public float jumpHeight = 3f;
    public LayerMask groundMask;
    UnityEngine.Vector3 velocity;


    // Update is called once per frame
    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.transform.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Debug.Log("Jump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        if (Input.GetButtonDown("Run"))
        {
            speed = sprintSpeed;
        }
        else if(Input.GetButtonUp("Run"))
        {
            speed = walkSpeed;
        }
        float xMov = Input.GetAxisRaw("Vertical");
        float zMov = -Input.GetAxisRaw("Horizontal");
        UnityEngine.Vector3 move = transform.forward * xMov - transform.right * zMov;

        controller.Move(move * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


    }
}