using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform cam;
    public float speed = 12;
    public CharacterController controller;
    public float smoothTime = 0.1f;
    float turnSmoothly;
    public LayerMask layermask;
    public GameObject[] linecastPoints;
    RaycastHit hit;

    [HideInInspector]
    public bool canMove = true;
    Vector3 moveDirection = Vector3.zero;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    float CayoteeTime = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 20;
        }
        else
        {
            speed = 12;
        }
        Vector3 direction;
        float movementDirectionY = moveDirection.y;

        if (Input.GetButton("Jump")/* && (canMove && controller.isGrounded)*/ && CayoteeTime <= 0.5f)
        {
            moveDirection.y = jumpSpeed;
            CayoteeTime = 1.0f;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (controller.isGrounded)
        {
            CayoteeTime = 0.0f;
        }

        controller.Move(moveDirection * Time.deltaTime);
        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!controller.isGrounded)
        {
            if (CayoteeTime <= 0.5f)
            {
                CayoteeTime += Time.deltaTime;
            }

            moveDirection.y -= gravity * Time.deltaTime;
        }
        direction = new Vector3(x, 0f, z).normalized;



        if (direction.magnitude >= 0.1f)
        {
            float targetAng = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAng, ref turnSmoothly, smoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAng, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }


    }

}


