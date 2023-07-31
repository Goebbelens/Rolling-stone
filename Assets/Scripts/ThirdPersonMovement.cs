using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThirdPersonMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private Transform cam;

    [Header("Movement, Gravity and Jump")]
    public float speed = 6f;
    public float jumpHeight = 3f;
    public float fallSpeed = 1f;
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = 1f;

    private float gravity = -9.81f;
    private Vector3 velocity;
    private bool isGrounded;

    [Header("Character rotation align to camera")]
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }
        
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        

        if (direction.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        if(Input.GetButtonDown("Fire3"))
        {

        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime * fallSpeed;
        controller.Move(velocity * Time.deltaTime);
    }
}
