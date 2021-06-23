using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    public float m_speed = 5f;
    public float rotateSpeed = 20f;
    public Vector3 m_Input;
    public GameObject playerMesh;
    public GameObject mainCamera;
    public float cameraYpos;
    public bool isDead;
    

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        float cameraXpos = this.transform.position.x - 3;
        float cameraZpos = this.transform.position.z;
        mainCamera.transform.position = new Vector3(cameraXpos, cameraYpos, cameraZpos);
    }

    void FixedUpdate()
    {
        if (isDead == false)
        { 
            var rotationVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            var rotation = Quaternion.LookRotation(rotationVector);
            m_Input = new Vector3(Input.GetAxis("Vertical"), 0, -Input.GetAxis("Horizontal"));
            m_Rigidbody.MovePosition(transform.position + m_Input * Time.deltaTime * m_speed);
            if ((rotationVector.x != 0) || (rotationVector.z != 0))
            {
                playerMesh.gameObject.transform.rotation = Quaternion.RotateTowards(playerMesh.gameObject.transform.rotation, rotation, rotateSpeed);
            }
        }
    }
}
