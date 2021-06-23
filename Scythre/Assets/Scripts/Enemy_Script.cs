using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Script : MonoBehaviour
{
    public float fov = 60f;
    public float viewdistance = 7f;
    public float chase_time_after_los = 7f;
    public float chase_time = 0f;

    private GameObject currentNode;

    public GameObject Node_1;
    public GameObject Node_2;
    public GameObject Node_3;
    public GameObject Node_4;
    public GameObject Node_5;
    public GameObject Node_6;

    public bool Player_found;

    private bool tracking_player_test;

    public GameObject Player;

    public GameObject Detected;



    private NavMeshAgent NMA;

    private RaycastHit hit;
    void Start()
    {
        NMA = GetComponent<NavMeshAgent>();
        NMA.destination = Node_1.transform.position;
        Player_found = false;
        Player = GameObject.FindGameObjectWithTag("Player");
        tracking_player_test = false;

        currentNode = Node_1;
    }

    
    void Update()
    {
        if (Player_found == false)
        {
            patrol();
        }
        if (Player_found == true)
        {
            Chase();
            tracking_player_test = true;
            Detected.SetActive(true);
        }
    }
    void patrol()
    {   
        if (tracking_player_test == true)
        {
            NMA.destination = currentNode.transform.position;
            chase_time = 0;
            Detected.SetActive(false);
            tracking_player_test = false;
        }

         if (Vector3.Distance(transform.position, Node_1.transform.position) < 1 & (currentNode == Node_1))
         {
            Node_1.SetActive(false);
            Node_2.SetActive(true);
            NMA.destination = Node_2.transform.position;
            currentNode = Node_2;
        }

         if (Vector3.Distance(transform.position, Node_2.transform.position) < 1 & (currentNode == Node_2))
         {
            Node_2.SetActive(false);
            Node_3.SetActive(true);
            NMA.destination = Node_3.transform.position;
            currentNode = Node_3;

        }

         if (Vector3.Distance(transform.position, Node_3.transform.position) < 1 & (currentNode == Node_3))
         {
            Node_3.SetActive(false);
            Node_4.SetActive(true);
            NMA.destination = Node_4.transform.position;
            currentNode = Node_4;

        }

        if (Vector3.Distance(transform.position, Node_4.transform.position) < 1 & (currentNode == Node_4))
        {
            Node_4.SetActive(false);
            Node_5.SetActive(true);
            NMA.destination = Node_5.transform.position;
            currentNode = Node_5;

        }

        if (Vector3.Distance(transform.position, Node_5.transform.position) < 1 & (currentNode == Node_5))
        {
            Node_5.SetActive(false);
            Node_6.SetActive(true);
            NMA.destination = Node_6.transform.position;
            currentNode = Node_6;

        }

        if (Vector3.Distance(transform.position, Node_6.transform.position) < 1 & (currentNode == Node_6))
        {
            Node_6.SetActive(false);
            Node_1.SetActive(true);
            NMA.destination = Node_1.transform.position;
            currentNode = Node_1;

        }

        if (Vector3.Distance(Player.transform.position, transform.position) < viewdistance)
        {
            if (Vector3.Angle(Player.transform.position - transform.position, transform.forward) <= fov / 2)
            {
                if (Physics.Linecast(transform.position, Player.transform.position, out hit))
                {
                    if (hit.collider.name == "Player")
                    {
                        if (Player.GetComponent<Player_Movement>().isDead == false)
                        {
                            Debug.Log("it works");
                            Player_found = true;
                        }
                    }
                }
            }
        }
    }

    void Chase()
    {
        NMA.destination = Player.transform.position;
        if (Vector3.Distance(Player.transform.position, transform.position) < viewdistance)
        {
            if (Vector3.Angle(Player.transform.position - transform.position, transform.position) <= fov / 2)
            {
                if (Physics.Linecast(transform.position, Player.transform.position, out hit))
                {
                    if (hit.collider.name == "Player")
                    {
                        Player_found = true;
                        chase_time = 0;
                    }
                }
            }
        }

        if (chase_time < chase_time_after_los)
        {
            chase_time += Time.deltaTime;
        }
        if (chase_time > chase_time_after_los)
        {
            Player_found = false;
        }


    }
}
