using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    public CharacterController controller;
    private Vector3 rawInputMovement;
    private Vector3 smoothInputMovement;


    public float m_speed = 6f;
    public float jump_height = 1.5f;
    public float gravity_value = -9.81f;


    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public float rotateSpeed = 20f;

    private Vector3 playerVelocity;

    public PlayerInput playerInput;

    public GameObject playerMesh;
    public Transform mainCamera;
    public bool isDead;
    public bool playerGrounded;
    

    // Start is called before the first frame update
    void Start()
    {
        controller = playerMesh.GetComponent<CharacterController>();
        playerInput = playerMesh.GetComponent<PlayerInput>();
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {

        playerVelocity.y += gravity_value * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
        //if (inputMovement != Vector2.zero)
        //{
        //    float targetAngle = Mathf.Atan2(inputMovement.x,inputMovement.y) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;
        //    Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
        //    // float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        //    playerMesh.gameObject.transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);
        //}
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        if (playerGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jump_height * -3.0f * gravity_value);
        }
    }
    public void OnAttack(InputAction.CallbackContext value)
    {

    }
    public void OnGrapple(InputAction.CallbackContext value)
    {

    }

    void FixedUpdate()
    {
        if (isDead == false)
        {
            playerGrounded = controller.isGrounded;
            if (playerGrounded && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }


            Vector3 m_Input = rawInputMovement;
            if (m_Input.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(m_Input.x, m_Input.z) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;
                Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                playerMesh.gameObject.transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);

                Vector3 move_Dir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(move_Dir.normalized * Time.deltaTime * m_speed);
            }
        }
    }
}
