using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    public CharacterController controller;
    public float m_speed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
  //  public float rotateSpeed = 20f;


    public GameObject playerMesh;
    public Transform mainCamera;
    public float cameraYpos;
    public bool isDead;
    

    // Start is called before the first frame update
    void Start()
    {
        controller = playerMesh.GetComponent<CharacterController>();
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
      //  float cameraXpos = this.transform.position.x - 3;
      //  float cameraZpos = this.transform.position.z;
      //  mainCamera.transform.position = new Vector3(cameraXpos, cameraYpos, cameraZpos);
    }

    void FixedUpdate()
    {
        if (isDead == false)
        {
            //  var rotationVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            //  var rotation = Quaternion.LookRotation(rotationVector);
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

           Vector3 m_Input = new Vector3(horizontal, 0, vertical).normalized;
            if (m_Input.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(m_Input.x, m_Input.z) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                playerMesh.gameObject.transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 move_Dir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(move_Dir.normalized * Time.deltaTime * m_speed);
            }
        //    if ((rotationVector.x != 0) || (rotationVector.z != 0))
      //      {
       //         playerMesh.gameObject.transform.rotation = Quaternion.RotateTowards(playerMesh.gameObject.transform.rotation, rotation, rotateSpeed);
       //     }
        }
    }
}
